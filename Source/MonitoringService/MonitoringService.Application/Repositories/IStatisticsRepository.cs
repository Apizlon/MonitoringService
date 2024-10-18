using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

public interface IStatisticsRepository
{
    Task<ulong> AddStatAsync(Statistics statistics);
    Task DeleteStatAsync(ulong id);
    Task<Statistics> GetStatAsync(ulong id);
    Task<IEnumerable<Statistics>> GetStatsAsync();
    Task UpdateStatAsync(ulong id, Statistics statistics);
    Task<bool> StatExistsAsync(ulong id);
}