using Microsoft.Extensions.DependencyInjection;
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
            .AddScoped<IStatisticsRepository,StatisticsRepository>();
    }
}