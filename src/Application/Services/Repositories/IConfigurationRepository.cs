using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Services.Repositories;

public interface IConfigurationRepository
{

    Task CreateConfigurationAsync(ConfigurationItem configurationItem);

    Task<IEnumerable<ConfigurationItem>> GetAllConfigurationsAsync(string applicationName);
    Task<IEnumerable<ConfigurationItem>> GetAllConfigurationsAsync();
    Task<ConfigurationItem> GetConfigurationByIdAsync(string id);
   
    Task<bool> UpdateConfigurationAsync(ConfigurationItem configurationItem);
    Task<bool> DeleteAsync(string id);
}
