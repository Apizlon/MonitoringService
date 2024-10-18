using Microsoft.Extensions.DependencyInjection;
using MonitoringService.Application.Models;
using MonitoringService.Application.Processors;

namespace MonitoringService.Application.Extensions;

public static class ProcessorsExtension
{
    public static IServiceCollection AddProcessors(this IServiceCollection services)
    {
        return services
            .AddScoped<StatisticsProcessor>();
    }
}