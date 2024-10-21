using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Mappers;

/// <summary>
/// Класс-расширение для маппинга
/// </summary>
public static class StatisticsMapper
{
    /// <summary>
    /// Маппинг <see cref="StatisticsRequest"/> к <see cref="Statistics"/>
    /// </summary>
    /// <param name="statisticsRequest"><see cref="StatisticsRequest"/></param>
    /// <returns><see cref="Statistics"/></returns>
    public static Statistics MapToDomain(this StatisticsRequest statisticsRequest)
    {
        return new Statistics()
        {
            DeviceName = statisticsRequest.DeviceName,
            OperatingSystem = statisticsRequest.OperatingSystem,
            Version = statisticsRequest.Version,
            LastUpdateDateTime = DateTime.Now
        };
    }
    
    /// <summary>
    /// Маппинг <see cref="Statistics"/> к <see cref="StatisticsResponse"/>
    /// </summary>
    /// <param name="statistics"><see cref="Statistics"/></param>
    /// <returns><see cref="StatisticsResponse"/></returns>
    public static StatisticsResponse MapToContract(this Statistics statistics)
    {
        return new StatisticsResponse()
        {
            Id = statistics.Id,
            DeviceName = statistics.DeviceName,
            OperatingSystem = statistics.OperatingSystem,
            Version = statistics.Version,
            LastUpdateDateTime = statistics.LastUpdateDateTime
        };
    }
    
    /// <summary>
    /// Маппинг набора объектов <see cref="Statistics"/> к <see cref="StatisticsResponse"/>
    /// </summary>
    /// <param name="statistics">Набор объектов <see cref="Statistics"/></param>
    /// <returns>Набор объектов <see cref="StatisticsResponse"/></returns>
    public static IEnumerable<StatisticsResponse> MapToContract(this IEnumerable<Statistics> statistics)
    {
        return statistics.Select(x => x.MapToContract());
    }
}