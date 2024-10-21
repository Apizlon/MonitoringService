using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Mappers;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Validator;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;

    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    public async Task<ulong> AddStatisticsAsync(StatisticsRequest statisticsRequest)
    {
        statisticsRequest.Validate();
        return await _statisticsRepository.AddStatAsync(statisticsRequest.MapToDomain());
    }

    public async Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync()
    {
        var allStatistics = await _statisticsRepository.GetStatsAsync();
        return allStatistics.MapToContract();
    }

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