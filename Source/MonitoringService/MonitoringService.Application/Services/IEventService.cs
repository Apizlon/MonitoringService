using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

/// <summary>
/// Сервис для управления объектами типа <see cref="StatEvent"/>>
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Дабавление события.
    /// При добавлении события обновляется последнее время обновления статистики.
    /// Для реализации используется транзакция
    /// </summary>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    /// <returns>id добавленного события</returns>
    Task<Guid> AddEventAsync(EventRequest eventRequest);
    
    /// <summary>
    /// Удаления события
    /// </summary>
    /// <param name="id">id события</param>
    Task DeleteEventAsync(Guid id);
    
    /// <summary>
    /// Получение объекта по id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="EventResponse"/>></returns>
    Task<EventResponse> GetEventAsync(Guid id);
    
    /// <summary>
    /// Получения событий по id статистики
    /// </summary>
    /// <param name="statisticsId">id статистики</param>
    /// <returns>IEnumerable объектов типа <see cref="EventResponse"/></returns>
    Task<IEnumerable<EventResponse>> GetEventsByStatisticsIdAsync(int statisticsId);
    
    /// <summary>
    /// Обновление события
    /// При обновлении события обновляется последнее время обновления статистики.
    /// Для реализации используется транзакция
    /// </summary>
    /// <param name="id">id события</param>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    Task UpdateEventAsync(Guid id, EventRequest eventRequest);
}