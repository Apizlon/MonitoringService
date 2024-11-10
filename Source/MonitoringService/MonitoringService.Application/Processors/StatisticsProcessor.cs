using MonitoringService.Application.Models;
using MonitoringService.Application.Services;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Processors;

/// <summary>
/// Процессор для работы со статистикой
/// </summary>
public class StatisticsProcessor
{
    /// <see cref="StatisticsService"/>
    private readonly IStatisticsService _statisticsService;

    /// <summary>
    /// Конструктор с одним параметром
    /// </summary>
    /// <param name="statisticsService"><see cref="IStatisticsService"/></param>
    public StatisticsProcessor(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    
    /// <summary>
    /// Добавление статистики
    /// </summary>
    /// <param name="statisticsRequest"></param>
    /// <returns></returns>
    public async Task<int> AddStatisticsAsync(StatisticsRequest statisticsRequest)
    {
        return await _statisticsService.AddStatisticsAsync(statisticsRequest);
    }
    
    /// <summary>
    /// Получение всей статистики
    /// </summary>
    /// <returns>>IEnumerable объектов типа <see cref="StatisticsResponse"/></returns>
    public async Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync()
    {
        return await _statisticsService.GetAllStatisticsAsync();
    }
    
    /// <summary>
    /// Получение объекта по id
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="StatisticsResponse"/>></returns>
    public async Task<StatisticsResponse> GetStatisticsAsync(int id)
    {
        return await _statisticsService.GetStatisticsAsync(id);
    }
    
    /// <summary>
    /// Обновление статистики
    /// </summary>
    /// <param name="id">id обновляемого элемента</param>
    /// <param name="statisticsRequest">Объект обновленной статистики</param>
    public async Task UpdateStatisticsAsync(int id, StatisticsRequest statisticsRequest)
    {
        await _statisticsService.UpdateStatisticsAsync(id, statisticsRequest);
    }
    
    /// <summary>
    /// Удаление статистики
    /// </summary>
    /// <param name="id">id удаляемого элемента</param>
    public async Task DeleteStatisticsAsync(int id)
    {
        await _statisticsService.DeleteStatisticsAsync(id);
    }
}