using Microsoft.Extensions.DependencyInjection;
using MonitoringService.Application.Models;
using MonitoringService.Application.Processors;

namespace MonitoringService.Application.Extensions;

/// <summary>
/// Класс-расширение для добавления процессоров
/// </summary>
public static class ProcessorsExtension
{
    /// <summary>
    /// Метод для добавления процессоров
    /// </summary>
    public static IServiceCollection AddProcessors(this IServiceCollection services)
    {
        return services
            .AddScoped<StatisticsProcessor>()
            .AddScoped<EventProcessor>();
    }
}