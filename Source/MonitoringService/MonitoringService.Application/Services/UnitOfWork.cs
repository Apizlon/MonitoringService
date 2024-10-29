using System.Data;
using Microsoft.Extensions.Configuration;
using MonitoringService.Application.Repositories;
using Npgsql;

namespace MonitoringService.Application.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnection _connection;
    private IDbTransaction _transaction;
    private bool _disposed;
    
    public IStatisticsRepository _StatisticsRepository { get; }
    public IEventRepository _EventRepository { get; }

    public UnitOfWork(IDbConnection connection,IStatisticsRepository statisticsRepository,IEventRepository eventRepository)
    {
        _connection = connection;
        _connection.Open();
        _StatisticsRepository = statisticsRepository;
        _EventRepository = eventRepository;
    }

    public void BeginTransaction()
    {
        _transaction = _connection.BeginTransaction();
        _StatisticsRepository.SetTransaction(_transaction);
        _EventRepository.SetTransaction(_transaction);
    }

    public void Commit()
    {
        _transaction?.Commit();
        _transaction = null;
    }

    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction = null;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _transaction?.Dispose();
            _connection?.Dispose();
            _disposed = true;
        }
    }
}