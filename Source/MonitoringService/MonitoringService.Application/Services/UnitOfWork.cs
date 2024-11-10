using System.Data;
using Microsoft.Extensions.Configuration;
using MonitoringService.Application.Repositories;
using Npgsql;

namespace MonitoringService.Application.Services;

/// <inheritdoc />
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// <see cref="IDbConnection"/>
    /// </summary>
    private readonly IDbConnection _connection;
    
    /// <summary>
    /// <see cref="IDbTransaction"/>
    /// </summary>
    private IDbTransaction _transaction;
    
    /// <summary>
    /// Флаг очищенности ресурсов
    /// </summary>
    private bool _disposed;
    
    /// <inheritdoc />
    public IStatisticsRepository _StatisticsRepository { get; }
    
    /// <inheritdoc />
    public IEventRepository _EventRepository { get; }

    /// <summary>
    /// Конструктор с 3 параметрами(получаются из DI)
    /// </summary>
    /// <param name="connection">Подключение к базе данных</param>
    /// <param name="statisticsRepository"><see cref="StatisticsRepository"/></param>
    /// <param name="eventRepository"><see cref="EventRepository"/></param>
    public UnitOfWork(IDbConnection connection,IStatisticsRepository statisticsRepository,IEventRepository eventRepository)
    {
        _connection = connection;
        _connection.Open();
        _StatisticsRepository = statisticsRepository;
        _EventRepository = eventRepository;
    }
    
    /// <inheritdoc />
    public void BeginTransaction()
    {
        _transaction = _connection.BeginTransaction();
        _StatisticsRepository.SetTransaction(_transaction);
        _EventRepository.SetTransaction(_transaction);
    }
    
    /// <inheritdoc />
    public void Commit()
    {
        _transaction?.Commit();
        _transaction = null;
    }
    
    /// <inheritdoc />
    public void Rollback()
    {
        _transaction?.Rollback();
        _transaction = null;
    }
    
    /// <summary>
    /// Реализация метода интерфейса <see cref="IDisposable"/>
    /// </summary>
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