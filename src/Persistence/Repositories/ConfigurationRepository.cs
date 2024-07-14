using Application.Services.Repositories;
using Domain.DTOs.MongoDb;
using Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Persistence.Repositories;

public class ConfigurationRepository: IConfigurationRepository
{
    #region Fields
    private readonly IMongoCollection<ConfigurationItem> _configurationRepository;
    #endregion

    #region Constructors
    public ConfigurationRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _configurationRepository = database.GetCollection<ConfigurationItem>(settings.Value.CollectionName);
    }

    #endregion

    #region Methods
    public async Task<ConfigurationItem> GetConfigurationByIdAsync(string id)
    {
        var builder = Builders<ConfigurationItem>.Filter;
        var filter = builder.Eq(c => c.Id, id);
        return await _configurationRepository.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var builder = Builders<ConfigurationItem>.Filter;
        var filter = builder.Eq(c=> c.Id, id);
        var result = await _configurationRepository.DeleteOneAsync(filter);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public async Task CreateConfigurationAsync(ConfigurationItem configurationItem)
    {
       await _configurationRepository.InsertOneAsync(configurationItem);
    }

    public async Task<IEnumerable<ConfigurationItem>> GetAllConfigurationsAsync(string applicationName)
    {
        var builder = Builders<ConfigurationItem>.Filter;
        var filter = builder.Eq(c=>c.ApplicationName, applicationName);
        return await _configurationRepository.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<ConfigurationItem>> GetAllConfigurationsAsync()
    {
        return await _configurationRepository.Find(c => true).ToListAsync();
    }

    public async Task<bool> UpdateConfigurationAsync(ConfigurationItem configurationItem)
    {
        var result = await _configurationRepository.ReplaceOneAsync(x => x.Id == configurationItem.Id, configurationItem, new ReplaceOptions() { IsUpsert = false });
        return result.IsAcknowledged && result.ModifiedCount> 0;
    }
    #endregion
}
