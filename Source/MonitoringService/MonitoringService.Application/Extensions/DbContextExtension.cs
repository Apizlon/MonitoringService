using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace MonitoringService.Application.Extensions;

/// <summary>
/// Класс расширение для <see cref="IDbConnection"/>
/// </summary>
public static class DbContextExtension
{
    /// <summary>
    /// Метод для добавления подключения к базе данных в DI.
    /// Если строка подключения определена в переменной окружения, то берется оттуда, иначе из appsettings
    /// </summary>
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