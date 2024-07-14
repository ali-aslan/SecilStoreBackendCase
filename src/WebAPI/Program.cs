using Persistence;
using Infrastructure;
using Application;
using Hangfire;
using Infrastructure.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddPresistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddTransient<ConfigurationReaderTask>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.UseHangfireDashboard();

//app.Services.GetRequiredService<IRecurringJobManager>().AddOrUpdate(
//   "CheckConfigUpdates",
//   () => app.Services.GetRequiredService<ConfigurationReader>().CheckForUpdatesAndNotify(),
//   Cron.MinuteInterval(1));


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var task = serviceProvider.GetRequiredService<ConfigurationReaderTask>();

    task.Execute(CancellationToken.None);
}

app.Run();
