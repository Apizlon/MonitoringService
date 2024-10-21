using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

/// <summary>
/// Интерфейс репозитория статистики
/// </summary>
public interface IStatisticsRepository
{
    /// <summary>
    /// Добавление статистики
    /// </summary>
    /// <param name="statistics"><see cref="Statistics"/>></param>
    /// <returns>id добавленного элемента</returns>
    Task<ulong> AddStatAsync(Statistics statistics);
    
    /// <summary>
    /// Удаление статистики
    /// </summary>
    /// <param name="id">id удаляемого элемента</param>
    Task DeleteStatAsync(ulong id);
    
    /// <summary>
    /// Получение объекта по id
    /// </summary>
    /// <param name="id">id</param>
    Task<Statistics> GetStatAsync(ulong id);
    
    /// <summary>
    /// Получение всей статистики
    /// </summary>
    /// <returns>IEnumerable объектов типа <see cref="Statistics"/>></returns>
    Task<IEnumerable<Statistics>> GetStatsAsync();
    
    /// <summary>
    /// Обновление статистики
    /// </summary>
    /// <param name="id">id обновляемого элемента</param>
    /// <param name="statistics">Объект обновленной статистики</param>
    Task UpdateStatAsync(ulong id, Statistics statistics);
    
    /// <summary>
    /// Проверка существования объекта статистики
    /// </summary>
    /// <param name="id">id искомого объекта</param>
    Task<bool> StatExistsAsync(ulong id);
}