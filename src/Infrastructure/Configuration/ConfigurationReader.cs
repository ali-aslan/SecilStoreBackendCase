using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Configuration;

public class ConfigurationReader
{
    #region Fields
    private readonly IMongoCollection<ConfigurationItem> _configurationRepository;
    private readonly ConcurrentDictionary<string, (string Value, DateTime LastUpdated)> _cache = new();
    private readonly TimeSpan _refreshInterval;
    private readonly string _applicationName;
    private CancellationTokenSource _cancellationTokenSource;
    private Task _refreshTask;
    #endregion

    #region Constructors
    public ConfigurationReader(string applicationName, string connectionString, int refreshTimerIntervalInMs)
    {
        _applicationName = applicationName;
        _refreshInterval = TimeSpan.FromMilliseconds(refreshTimerIntervalInMs);

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("SecilStoreTestDb");
        _configurationRepository = database.GetCollection<ConfigurationItem>("SecilStoreCollection");

        StartRefreshTask();
    }
    #endregion

    #region Methods

    public T GetValue<T>(string key)
    {
        var builder = Builders<ConfigurationItem>.Filter;
        var filterName = builder.Eq(c => c.Name, key);
        var filterActive = builder.Eq(c => c.IsActive, true);

        var configuration = _configurationRepository.Find(filterName & filterActive).FirstOrDefault();

        if (configuration == null)
        {
            throw new KeyNotFoundException($"Configuration key '{key}' not found for application '{_applicationName}'.");
        }

        try
        {
            return (T)Convert.ChangeType(configuration.Value, typeof(T));
        }
        catch (InvalidCastException)
        {
            throw new InvalidOperationException($"Configuration value for key '{key}' cannot be converted to type '{typeof(T).Name}'.");
        }
    }


    private void StartRefreshTask()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _refreshTask = Task.Run(async () =>
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await RefreshConfigurations();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred during configuration refresh: {ex.Message}");
                    // To Do Add Log
                }
                await Task.Delay(_refreshInterval, _cancellationTokenSource.Token);
            }
        }, _cancellationTokenSource.Token);
    }

    private void StopRefreshTask()
    {
        _cancellationTokenSource?.Cancel();
        _refreshTask?.Wait(); 
    }
    private async Task RefreshConfigurations()
    {
        try
        {
            var buider = Builders<ConfigurationItem>.Filter;

            var filterName = buider.Eq(c => c.ApplicationName, _applicationName);
            var filterActive = buider.Eq(c => c.IsActive, true);

            var filters = Builders<ConfigurationItem>.Filter.And(filterName, filterActive);

            var configurations = await _configurationRepository.Find(filters).ToListAsync();

            foreach (var config in configurations)
            {
                _cache.AddOrUpdate(config.Name, (config.Value, DateTime.UtcNow), (key, oldValue) => (config.Value, DateTime.UtcNow));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to refresh configurations from MongoDB: {ex.Message}. Proceeding with cached values.");
            // To Do Add Log
        }
    }

    public async Task CheckForUpdatesAndNotify()
    {
        var latestUpdate = _cache.Values.Any() ? _cache.Values.OrderByDescending(v => v.LastUpdated).First().LastUpdated : DateTime.MinValue;

        var buider = Builders<ConfigurationItem>.Filter;

        var filterName = buider.Eq(c => c.ApplicationName, _applicationName);
        var filterTime = buider.Gt(c => c.LastUpdated, latestUpdate);

        var filters = Builders<ConfigurationItem>.Filter.And(filterName, filterTime);

        var updates = await _configurationRepository.Find(filters).ToListAsync();

        foreach (var update in updates)
        {
            _cache.AddOrUpdate(update.Name, (update.Value, DateTime.UtcNow), (key, oldValue) => (update.Value, DateTime.UtcNow));
        }
    }
    #endregion
}

