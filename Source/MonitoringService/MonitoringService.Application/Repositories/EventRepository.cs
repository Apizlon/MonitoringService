using System.Data.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using MonitoringService.Application.Models;
using MonitoringService.Application.SqlScripts;
using Npgsql;

namespace MonitoringService.Application.Repositories;

public class EventRepository : IEventRepository
{
    private readonly string _dbConnection;
    
    public EventRepository(IConfiguration configuration)
    {
        _dbConnection = string.IsNullOrEmpty(configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"]) 
            ? configuration.GetConnectionString("StatisticsDatabaseConnection") 
            : configuration.GetSection("ConnectionStrings")["StatisticsDatabaseConnection"];
    }
    
    private async Task<DbConnection> CreateConnectionAsync()
    {
        var connection = new NpgsqlConnection(_dbConnection);
        await connection.OpenAsync();
        return connection;
    }

    public async Task<Guid> AddEventAsync(StatEvent statEvent)
    {
        await using var connection = await CreateConnectionAsync();
        var id = await connection.ExecuteScalarAsync<Guid>(Sql.AddEvent,statEvent);
        return id;
    }

    public async Task DeleteEventAsync(Guid id)
    {
        await using var connection = await CreateConnectionAsync();
        await connection.ExecuteAsync(Sql.DeleteEvent, new { Id = id });
    }

    public async Task<StatEvent> GetEventAsync(Guid id)
    {
        await using var connection = await CreateConnectionAsync();
        var statEvent = await connection.QuerySingleOrDefaultAsync<StatEvent>(Sql.GetEvent, new { Id = id });
        return statEvent;
    }

    public async Task<IEnumerable<StatEvent>> GetEventsByStatisticsIdAsync(int statisticsId)
    {
        await using var connection = await CreateConnectionAsync();
        var events = await connection.QueryAsync<StatEvent>(Sql.GetEventsByStatisticsId, new {StatisticsId = statisticsId});
        return events;
    }

    public async Task UpdateEventAsync(Guid id, StatEvent statEvent)
    {
        await using var connection = await CreateConnectionAsync();
        await connection.ExecuteAsync(Sql.UpdateEvent,
            new
            {
                Id = id, 
                statEvent.StatisticsId,
                statEvent.EventDateTime,
                statEvent.Name,
                statEvent.Description
            });
    }

    public async Task<bool> EventExistsAsync(Guid id)
    {
        await using var connection = await CreateConnectionAsync();
        var exists = await connection.ExecuteScalarAsync<bool>(Sql.ExistsEvent, new { Id = id });
        return exists;
    }
}