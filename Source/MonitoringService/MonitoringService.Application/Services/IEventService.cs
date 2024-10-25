using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

public interface IEventService
{
    Task<Guid> AddEventAsync(EventRequest eventRequest);
    Task DeleteEventAsync(Guid id);
    Task<EventResponse> GetEventAsync(Guid id);
    Task<IEnumerable<EventResponse>> GetEventsByStatisticsIdAsync(int statisticsId);
    Task UpdateEventAsync(Guid id, EventRequest eventRequest);
}