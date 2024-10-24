using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Mappers;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Validator;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

/// <inheritdoc />
public class StatisticsService : IStatisticsService
{
    /// <summary>
    /// <see cref="StatisticsRepository"/>>
    /// </summary>
    private readonly IStatisticsRepository _statisticsRepository;
    
    /// <summary>
    /// Конструктор с одним параметром
    /// </summary>
    /// <param name="statisticsRepository"><see cref="IStatisticsRepository"/></param>
    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }
    
    /// <inheritdoc />
    public async Task<int> AddStatisticsAsync(StatisticsRequest statisticsRequest)
    {
        statisticsRequest.Validate();
        Statistics statistics = statisticsRequest.MapToDomain();
        statistics.LastUpdateDateTime=DateTime.Now;
        return await _statisticsRepository.AddStatAsync(statistics);
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync()
    {
        var allStatistics = await _statisticsRepository.GetStatsAsync();
        return allStatistics.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task<StatisticsResponse> GetStatisticsAsync(int id)
    {
        var isStatExists = await _statisticsRepository.StatExistsAsync(id);
        if (!isStatExists)
        {
            throw new StatisticsNotFoundException(id);
        }
        var statistics = await _statisticsRepository.GetStatAsync(id);
        return statistics.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task UpdateStatisticsAsync(int id, StatisticsRequest statisticsRequest)
    {
        var isStatisticsExists = await _statisticsRepository.StatExistsAsync(id);
        if (!isStatisticsExists)
        {
            throw new StatisticsNotFoundException(id);
        }
        statisticsRequest.Validate();
        Statistics statistics = statisticsRequest.MapToDomain();
        statistics.LastUpdateDateTime = DateTime.Now;;
        await _statisticsRepository.UpdateStatAsync(id, statistics);
    }
    
    /// <inheritdoc />
    public async Task DeleteStatisticsAsync(int id)
    {
        var isStatisticsExists = await _statisticsRepository.StatExistsAsync(id);
        if (!isStatisticsExists)
        {
            throw new StatisticsNotFoundException(id);
        }

        await _statisticsRepository.DeleteStatAsync(id);
    }
}