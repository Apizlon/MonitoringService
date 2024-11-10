using Mapster;
using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Mappers;

/// <summary>
/// Класс-расширение для маппинга. Использует Mapster для автомаппинга.
/// </summary>
public static class EventMapper
{
    /// <summary>
    /// Маппинг <see cref="EventRequest"/> к <see cref="StatEvent"/>
    /// </summary>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    /// <returns><see cref="StatEvent"/></returns>
    public static StatEvent MapToDomain(this EventRequest eventRequest)
    {
        return eventRequest.Adapt<StatEvent>();
    }
    
    /// <summary>
    /// Маппинг <see cref="StatEvent"/> к <see cref="EventResponse"/>
    /// </summary>
    /// <param name="statEvent"><see cref="StatEvent"/></param>
    /// <returns><see cref="EventResponse"/></returns>
    public static EventResponse MapToContract(this StatEvent statEvent)
    {
        return statEvent.Adapt<EventResponse>();
    }
    
    /// <summary>
    /// Маппинг набора объектов <see cref="StatEvent"/> к <see cref="EventResponse"/>
    /// </summary>
    /// <param name="events">Набор объектов <see cref="StatEvent"/></param>
    /// <returns>Набор объектов <see cref="EventResponse"/></returns>
    public static IEnumerable<EventResponse> MapToContract(this IEnumerable<StatEvent> events)
    {
        return events.Select(x => x.MapToContract());
    }
}