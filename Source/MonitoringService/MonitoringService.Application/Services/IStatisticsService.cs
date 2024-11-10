using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

/// <summary>
/// Сервис для управления объектами типа <see cref="Statistics"/>>
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// Добавление статистики
    /// </summary>
    /// <param name="statisticsRequest"><see cref="StatisticsRequest"/>></param>
    /// <returns>id добавленной статистики</returns>
    Task<int> AddStatisticsAsync(StatisticsRequest statisticsRequest);
    
    /// <summary>
    /// Получение всей статистики
    /// </summary>
    /// <returns>>IEnumerable объектов типа <see cref="StatisticsResponse"/></returns>
    Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync();
    
    /// <summary>
    /// Получение объекта по id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="StatisticsResponse"/>></returns>
    Task<StatisticsResponse> GetStatisticsAsync(int id);
    
    /// <summary>
    /// Обновление статистики
    /// </summary>
    /// <param name="id">id обновляемого элемента</param>
    /// <param name="statisticsRequest">Объект обновленной статистики</param>
    Task UpdateStatisticsAsync(int id, StatisticsRequest statisticsRequest);
    
    /// <summary>
    /// Удаление статистики
    /// </summary>
    /// <param name="id">id удаляемого элемента</param>
    Task DeleteStatisticsAsync(int id);
}