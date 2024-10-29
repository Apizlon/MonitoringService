using System.Data;
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
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    
    public StatisticsRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    
    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }
    
    /// <inheritdoc />
    public async Task<int> AddStatAsync(Statistics statistics)
    {
        var id = await _connection.ExecuteScalarAsync<int>(Sql.AddStatistics, statistics,_transaction);
        return id;
    }
    
    /// <inheritdoc />
    public async Task DeleteStatAsync(int id)
    {
        await _connection.ExecuteAsync(Sql.DeleteStatistics, new { Id = id },_transaction);
    }
    
    /// <inheritdoc />
    public async Task<Statistics> GetStatAsync(int id)
    {
        var statistics = await _connection.QuerySingleOrDefaultAsync<Statistics>(Sql.GetStatistics, new { Id = id },_transaction);
        return statistics;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<Statistics>> GetStatsAsync()
    {
        var allStats = await _connection.QueryAsync<Statistics>(Sql.GetAllStatistics,_transaction);
        return allStats;
    }
    
    /// <inheritdoc />
    public async Task UpdateStatAsync(int id, Statistics statistics)
    {
        await _connection.ExecuteAsync(Sql.UpdateStatistics,
            new
            {
                Id = id, 
                statistics.DeviceName,
                statistics.OperatingSystem,
                statistics.Version,
                statistics.LastUpdateDateTime
            },_transaction);
    }
    
    /// <inheritdoc />
    public async Task UpdateStatLastUpdateDateTimeAsync(int id, DateTime lastUpdateDateTime)
    {
        await _connection.ExecuteAsync(Sql.UpdateStatisticsLastUpdateDateTime,
            new { Id = id, LastUpdateDateTime = lastUpdateDateTime },_transaction);
    }
    
    /// <inheritdoc />
    public async Task<bool> StatExistsAsync(int id)
    {
        var exists = await _connection.ExecuteScalarAsync<bool>(Sql.ExistsStatistics, new { Id = id },_transaction);
        return exists;
    }
}