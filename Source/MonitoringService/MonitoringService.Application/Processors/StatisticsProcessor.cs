using MonitoringService.Application.Models;
using MonitoringService.Application.Services;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Processors;

public class StatisticsProcessor
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsProcessor(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    public async Task<ulong> AddStatisticsAsync(StatisticsRequest statisticsRequest)
    {
        return await _statisticsService.AddStatisticsAsync(statisticsRequest);
    }

    public async Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync()
    {
        return await _statisticsService.GetAllStatisticsAsync();
    }

    public async Task<StatisticsResponse> GetStatisticsAsync(ulong id)
    {
        return await _statisticsService.GetStatisticsAsync(id);
    }

    public async Task UpdateStatisticsAsync(ulong id, StatisticsRequest statisticsRequest)
    {
        await _statisticsService.UpdateStatisticsAsync(id, statisticsRequest);
    }

    public async Task DeleteStatisticsAsync(ulong id)
    {
        await _statisticsService.DeleteStatisticsAsync(id);
    }
}