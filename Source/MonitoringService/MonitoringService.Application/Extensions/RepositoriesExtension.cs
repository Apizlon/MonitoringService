using Microsoft.Extensions.DependencyInjection;
using MonitoringService.Application.Repositories;

namespace MonitoringService.Application.Extensions;

public static class RepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IStatisticsRepository,StatisticsRepository>();
    }
}