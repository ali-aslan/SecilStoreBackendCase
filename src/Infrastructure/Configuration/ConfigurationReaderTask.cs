using Infrastructure.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration;

public class ConfigurationReaderTask:ITask
{
    public int TimeInterval { get; } = 5000;
    private readonly IServiceProvider _serviceProvider;


    public ConfigurationReaderTask(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // Task'ın işlemi burada gerçekleştirilir
            var configurationReader = _serviceProvider.GetRequiredService<ConfigurationReader>();
            await configurationReader.CheckForUpdatesAndNotify();

            // Belirtilen aralıkta bekleyin
            await Task.Delay(TimeInterval, cancellationToken);
        }
    }

}
