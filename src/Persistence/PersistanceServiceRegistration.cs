using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Domain.DTOs.MongoDb;

using MongoDB.Driver;
using Persistence.Repositories;
using System;
using Application.Services.Repositories;


namespace Persistence;

public static class PersistanceServiceRegistration
{
    public static IServiceCollection AddPresistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDBConnection"));

        services.AddSingleton<IMongoDatabase>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = new MongoClient(settings.ConnectionString);
            return client.GetDatabase(settings.DatabaseName);
        });

        services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

        return services;
    }

}
