using Microsoft.Extensions.DependencyInjection;
using MonitoringService.Application.Services;

namespace MonitoringService.Application.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IStatisticsService,StatisticsService>();
    }
}