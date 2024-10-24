using System.Data.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using MonitoringService.Application.Models;
using MonitoringService.Application.SqlScripts;
using Npgsql;

namespace MonitoringService.Application.Repositories;

/// <inheritdoc />
public class StatisticsRepository : IStatisticsRepository
{
    /// <summary>
    /// Строка подключения
    /// </summary>
    private readonly string _dbConnection;
    
    /// <summary>
    /// Конструктор с одним параметром. Устанавливает строку подключения.
    /// Если нет переменной окружения строка подклчючения берется из appsettings
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public StatisticsRepository(IConfiguration configuration)
    {
        _dbConnection = string.IsNullOrEmpty(configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"]) 
            ? configuration.GetConnectionString("StatisticsDatabaseConnection") 
            : configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"];
    }
    
    /// <summary>
    /// Создание подкючения
    /// </summary>
    /// <returns>Подключние к базе данных</returns>
    private async Task<DbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_dbConnection);
        await connection.OpenAsync();
        return connection;
    }
    
    /// <inheritdoc />
    public async Task<int> AddStatAsync(Statistics statistics)
    {
        await using var connection = await CreateConnectionAsync();
        var id = await connection.ExecuteScalarAsync<int>(Sql.AddStatistics, statistics);
        return id;
    }
    
    /// <inheritdoc />
    public async Task DeleteStatAsync(int id)
    {
        await using var connection = await CreateConnectionAsync();
        await connection.ExecuteAsync(Sql.DeleteStatistics, new { Id = id });
    }
    
    /// <inheritdoc />
    public async Task<Statistics> GetStatAsync(int id)
    {
        await using var connection = await CreateConnectionAsync();
        var statistics = await connection.QuerySingleOrDefaultAsync<Statistics>(Sql.GetStatistics, new { Id = id });
        return statistics;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<Statistics>> GetStatsAsync()
    {
        await using var connection = await CreateConnectionAsync();
        var allStats = await connection.QueryAsync<Statistics>(Sql.GetAllStatistics);
        return allStats;
    }
    
    /// <inheritdoc />
    public async Task UpdateStatAsync(int id, Statistics statistics)
    {
        await using var connection = await CreateConnectionAsync();
        await connection.ExecuteAsync(Sql.UpdateStatistics,
            new
            {
                Id = id, 
                statistics.DeviceName,
                statistics.OperatingSystem,
                statistics.Version,
                statistics.LastUpdateDateTime
            });
    }
    
    /// <inheritdoc />
    public async Task<bool> StatExistsAsync(int id)
    {
        await using var connection = await CreateConnectionAsync();
        var exists = await connection.ExecuteScalarAsync<bool>(Sql.ExistsStatistics, new { Id = id });
        return exists;
    }
}