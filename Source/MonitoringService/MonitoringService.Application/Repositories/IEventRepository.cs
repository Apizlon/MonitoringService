using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

public interface IEventRepository
{
    Task<Guid> AddEventAsync(StatEvent statEvent);
    Task DeleteEventAsync(Guid id);
    Task<StatEvent> GetEventAsync(Guid id);
    Task<IEnumerable<StatEvent>> GetEventsByStatisticsIdAsync(int statisticsId);
    Task UpdateEventAsync(Guid id, StatEvent statEvent);
    Task<bool> EventExistsAsync(Guid id);
}