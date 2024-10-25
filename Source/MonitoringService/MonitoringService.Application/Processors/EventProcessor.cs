using MonitoringService.Application.Services;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Processors;

public class EventProcessor
{
    private readonly IEventService _eventService;

    public EventProcessor(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    public async Task<Guid> AddEventAsync(EventRequest eventRequest)
    {
        return await _eventService.AddEventAsync(eventRequest);
    }

    public async Task DeleteEventAsync(Guid id)
    {
        await _eventService.DeleteEventAsync(id);
    }

    public async Task<EventResponse> GetEventAsync(Guid id)
    {
        return await _eventService.GetEventAsync(id);
    }

    public async Task<IEnumerable<EventResponse>> GetEventsByStatisticsIdAsync(int statisticsId)
    {
        return await _eventService.GetEventsByStatisticsIdAsync(statisticsId);
    }

    public async Task UpdateEventAsync(Guid id, EventRequest eventRequest)
    {
        await _eventService.UpdateEventAsync(id, eventRequest);
    }
}