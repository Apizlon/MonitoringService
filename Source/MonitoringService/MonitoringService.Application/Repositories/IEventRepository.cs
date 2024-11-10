using System.Data;
using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

/// <summary>
/// Репозиторий события
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Добавление события
    /// </summary>
    /// <param name="statEvent"><see cref="StatEvent"/></param>
    /// <returns>guid добавленного соьытия</returns>
    Task<Guid> AddEventAsync(StatEvent statEvent);
    
    /// <summary>
    /// Удаление события
    /// </summary>
    /// <param name="id">id удаляемого элемента</param>
    Task DeleteEventAsync(Guid id);
    
    /// <summary>
    /// Получение события по id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="StatEvent"/></returns>
    Task<StatEvent> GetEventAsync(Guid id);
    
    /// <summary>
    /// Получения событий по id статистики
    /// </summary>
    /// <param name="statisticsId">id статистики</param>
    /// <returns>IEnumerable объектов типа <see cref="StatEvent"/></returns>
    Task<IEnumerable<StatEvent>> GetEventsByStatisticsIdAsync(int statisticsId);
    
    /// <summary>
    /// Обновление события
    /// </summary>
    /// <param name="id">id события</param>
    /// <param name="statEvent"><see cref="StatEvent"/></param>
    Task UpdateEventAsync(Guid id, StatEvent statEvent);
    
    /// <summary>
    /// Проверка существования объекта события
    /// </summary>
    /// <param name="id">id события</param>
    /// <returns>true если существует, иначе - false</returns>
    Task<bool> EventExistsAsync(Guid id);
    
    /// <summary>
    /// Установка транзакции
    /// </summary>
    /// <param name="transaction">объекта <see cref="IDbTransaction"/></param>
    void SetTransaction(IDbTransaction transaction);
}