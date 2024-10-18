using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

public interface IStatisticsService
{
    Task<ulong> AddStatisticsAsync(StatisticsRequest statisticsRequest);
    Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync();
    Task<StatisticsResponse> GetStatisticsAsync(ulong id);
    Task UpdateStatisticsAsync(ulong id, StatisticsRequest statisticsRequest);
    Task DeleteStatisticsAsync(ulong id);
}