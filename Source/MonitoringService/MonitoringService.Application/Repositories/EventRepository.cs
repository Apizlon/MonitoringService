using System.Data;
using System.Data.Common;
using Dapper;
using Microsoft.Extensions.Configuration;
using MonitoringService.Application.Models;
using MonitoringService.Application.SqlScripts;
using Npgsql;

namespace MonitoringService.Application.Repositories;

public class EventRepository : IEventRepository
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    
    public EventRepository(IDbConnection newConnection)
    {
        _connection = newConnection;
    }

    public void SetTransaction(IDbTransaction transaction)
    {
        _transaction = transaction;
    }
    
    public async Task<Guid> AddEventAsync(StatEvent statEvent)
    {
        var id = await _connection.ExecuteScalarAsync<Guid>(Sql.AddEvent,statEvent,_transaction);
        return id;
    }

    public async Task DeleteEventAsync(Guid id)
    {
        await _connection.ExecuteAsync(Sql.DeleteEvent, new { Id = id },_transaction);
    }

    public async Task<StatEvent> GetEventAsync(Guid id)
    {
        var statEvent = await _connection.QuerySingleOrDefaultAsync<StatEvent>(Sql.GetEvent, new { Id = id },_transaction);
        return statEvent;
    }

    public async Task<IEnumerable<StatEvent>> GetEventsByStatisticsIdAsync(int statisticsId)
    {
        var events = await _connection.QueryAsync<StatEvent>(Sql.GetEventsByStatisticsId, new {StatisticsId = statisticsId},_transaction);
        return events;
    }

    public async Task UpdateEventAsync(Guid id, StatEvent statEvent)
    {
        await _connection.ExecuteAsync(Sql.UpdateEvent,
            new
            {
                Id = id, 
                statEvent.StatisticsId,
                statEvent.EventDateTime,
                statEvent.Name,
                statEvent.Description
            },_transaction);
    }

    public async Task<bool> EventExistsAsync(Guid id)
    {
        var exists = await _connection.ExecuteScalarAsync<bool>(Sql.ExistsEvent, new { Id = id },_transaction);
        return exists;
    }
}