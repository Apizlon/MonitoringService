using MonitoringService.Application.Repositories;

namespace MonitoringService.Application.Services;

/// <summary>
/// Реализация паттерна UnitOFWork для упровления репозиториями статистики и событий
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <see cref="StatisticsRepository"/>
    IStatisticsRepository _StatisticsRepository { get; }
    
    /// <see cref="EventRepository"/>
    IEventRepository _EventRepository { get; }
    
    /// <summary>
    /// Начать транзакцию
    /// </summary>
    void BeginTransaction();
    
    /// <summary>
    /// Подтвердить изменения
    /// </summary>
    void Commit();
    
    /// <summary>
    /// Откат изменений
    /// </summary>
    void Rollback();
}