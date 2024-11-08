using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace MonitoringService.Application.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = string.IsNullOrEmpty(configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"]) 
            ? configuration.GetConnectionString("StatisticsDatabaseConnection") 
            : configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"];
        if (string.IsNullOrEmpty(connectionString))
        {
            Console.WriteLine("Внимание!Пустая строка подключения");
            return services;
        }
        return services.AddScoped<IDbConnection>(sp => new NpgsqlConnection(connectionString));
    }
}