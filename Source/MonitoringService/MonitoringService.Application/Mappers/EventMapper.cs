using Mapster;
using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Mappers;

public static class EventMapper
{
    public static StatEvent MapToDomain(this EventRequest eventRequest)
    {
        return eventRequest.Adapt<StatEvent>();
    }

    public static EventResponse MapToContract(this StatEvent statEvent)
    {
        return statEvent.Adapt<EventResponse>();
    }
    
    public static IEnumerable<EventResponse> MapToContract(this IEnumerable<StatEvent> events)
    {
        return events.Select(x => x.MapToContract());
    }
}