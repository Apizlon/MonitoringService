using Microsoft.Extensions.DependencyInjection;
using MonitoringService.Application.Services;

namespace MonitoringService.Application.Extensions;

/// <summary>
/// Класс-расширение для добавления сервисов
/// </summary>
public static class ServicesExtension
{
    /// <summary>
    /// Метод для добавления сервисов
    /// </summary>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IStatisticsService,StatisticsService>();
    }
}