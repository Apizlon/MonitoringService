using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MonitoringService.Application.Migrations;
using MonitoringService.Application.Repositories;

namespace MonitoringService.Application.Extensions;

/// <summary>
/// Класс-расширение для добавления репозиториев
/// </summary>
public static class RepositoriesExtension
{
    /// <summary>
    /// Метод для добавления репозиториев
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IStatisticsRepository, StatisticsRepository>()
            .AddScoped<IEventRepository, EventRepository>();
    }
    
    /// <summary>
    /// Метод для применения миграций
    /// </summary>
    public static IServiceCollection AddMigration(this IServiceCollection services, IConfiguration configuration)
    {
        string dbConnection = string.IsNullOrEmpty(configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"]) 
            ? configuration.GetConnectionString("StatisticsDatabaseConnection") 
            : configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"];
        return services
            .AddFluentMigratorCore()
            .ConfigureRunner( rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(dbConnection)
                .ScanIn(typeof(AddStatisticsTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
    }

}