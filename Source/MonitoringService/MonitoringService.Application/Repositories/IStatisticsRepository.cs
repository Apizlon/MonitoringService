using System.Data;
using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

/// <summary>
/// Репозиторий статистики
/// </summary>
public interface IStatisticsRepository
{
    /// <summary>
    /// Добавление статистики
    /// </summary>
    /// <param name="statistics"><see cref="Statistics"/>></param>
    /// <returns>id добавленного элемента</returns>
    Task<int> AddStatAsync(Statistics statistics);
    
    /// <summary>
    /// Удаление статистики
    /// </summary>
    /// <param name="id">id удаляемого элемента</param>
    Task DeleteStatAsync(int id);
    
    /// <summary>
    /// Получение объекта по id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="Statistics"/></returns>
    Task<Statistics> GetStatAsync(int id);
    
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
    Task UpdateStatAsync(int id, Statistics statistics);
    
    /// <summary>
    /// Обновление последнего времени обновления(при добавлении события)
    /// </summary>
    /// <param name="id">id статистики</param>
    /// <param name="lastUpdateDateTime">Время последнего обовления</param>
    /// <returns></returns>
    Task UpdateStatLastUpdateDateTimeAsync(int id, DateTime lastUpdateDateTime);
    
    /// <summary>
    /// Проверка существования объекта статистики
    /// </summary>
    /// <param name="id">id искомого объекта</param>
    Task<bool> StatExistsAsync(int id);
    
    /// <summary>
    /// Установка транзакции
    /// </summary>
    /// <param name="transaction">объекта <see cref="IDbTransaction"/></param>
    void SetTransaction(IDbTransaction transaction);
}