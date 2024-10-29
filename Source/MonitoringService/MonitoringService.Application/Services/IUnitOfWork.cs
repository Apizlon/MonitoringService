using MonitoringService.Application.Repositories;

namespace MonitoringService.Application.Services;

public interface IUnitOfWork : IDisposable
{
    IStatisticsRepository _StatisticsRepository { get; }
    IEventRepository _EventRepository { get; }
    void BeginTransaction();
    void Commit();
    void Rollback();
}