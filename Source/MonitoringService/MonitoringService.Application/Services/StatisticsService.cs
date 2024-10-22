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
    public async Task<ulong> AddStatisticsAsync(StatisticsRequest statisticsRequest)
    {
        statisticsRequest.Validate();
        return await _statisticsRepository.AddStatAsync(statisticsRequest.MapToDomain());
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync()
    {
        var allStatistics = await _statisticsRepository.GetStatsAsync();
        return allStatistics.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task<StatisticsResponse> GetStatisticsAsync(ulong id)
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
    public async Task UpdateStatisticsAsync(ulong id, StatisticsRequest statisticsRequest)
    {
        var isStatisticsExists = await _statisticsRepository.StatExistsAsync(id);
        if (!isStatisticsExists)
        {
            throw new StatisticsNotFoundException(id);
        }
        statisticsRequest.Validate();
        await _statisticsRepository.UpdateStatAsync(id, statisticsRequest.MapToDomain());
    }
    
    /// <inheritdoc />
    public async Task DeleteStatisticsAsync(ulong id)
    {
        var isStatisticsExists = await _statisticsRepository.StatExistsAsync(id);
        if (!isStatisticsExists)
        {
            throw new StatisticsNotFoundException(id);
        }

        await _statisticsRepository.DeleteStatAsync(id);
    }
}