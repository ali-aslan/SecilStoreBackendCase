using Domain.DTOs.ConfigurationReader;
using Domain.DTOs.MongoDb;
using Hangfire;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using Domain.DTOs.Hangfire;
using Infrastructure.Tasks;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ConfigurationReaderSettings>(configuration.GetSection("ConfigurationReaderSettings"));


        services.AddSingleton<ConfigurationReader>(serviceProvider =>
        {
            var settings = serviceProvider.GetService<IOptions<ConfigurationReaderSettings>>().Value;

            return new ConfigurationReader(
                applicationName: settings.ApplicationName,
                connectionString: settings.ConnectionString,
                refreshTimerIntervalInMs: settings.RefreshTimerIntervalInMs
            );
        });

        services.Configure<HangfireSettings>(configuration.GetSection("HangfireSettings"));
        var settings = services.BuildServiceProvider().GetRequiredService<IOptions<HangfireSettings>>().Value;

        services.AddHangfire(config => config.UseMongoStorage(settings.ConnectionString, settings.DatabaseName,
            new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new NoneMongoBackupStrategy()
                }
            }));

     

        services.AddTransient<ITask, ConfigurationReaderTask>();

        return services;
    }
}
