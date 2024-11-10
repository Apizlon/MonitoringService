using MonitoringService.Application.Services;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Processors;

/// <summary>
/// Процессор для работы с событиями
/// </summary>
public class EventProcessor
{
    /// <see cref="EventService"/>
    private readonly IEventService _eventService;
    
    /// <summary>
    /// Конструктор с одним параметром
    /// </summary>
    /// <param name="eventService"><see cref="EventService"/></param>
    public EventProcessor(IEventService eventService)
    {
        _eventService = eventService;
    }
    
    /// <summary>
    /// Дабавление события.
    /// </summary>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    /// <returns>id добавленного события</returns>
    public async Task<Guid> AddEventAsync(EventRequest eventRequest)
    {
        return await _eventService.AddEventAsync(eventRequest);
    }
    
    /// <summary>
    /// Удаления события
    /// </summary>
    /// <param name="id">id события</param>
    public async Task DeleteEventAsync(Guid id)
    {
        await _eventService.DeleteEventAsync(id);
    }
    
    /// <summary>
    /// Получение объекта по id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="EventResponse"/>></returns>
    public async Task<EventResponse> GetEventAsync(Guid id)
    {
        return await _eventService.GetEventAsync(id);
    }
    
    /// <summary>
    /// Получения событий по id статистики
    /// </summary>
    /// <param name="statisticsId">id статистики</param>
    /// <returns>IEnumerable объектов типа <see cref="EventResponse"/></returns>
    public async Task<IEnumerable<EventResponse>> GetEventsByStatisticsIdAsync(int statisticsId)
    {
        return await _eventService.GetEventsByStatisticsIdAsync(statisticsId);
    }
    
    /// <summary>
    /// Обновление события
    /// </summary>
    /// <param name="id">id события</param>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    public async Task UpdateEventAsync(Guid id, EventRequest eventRequest)
    {
        await _eventService.UpdateEventAsync(id, eventRequest);
    }
}