using Mapster;
using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Mappers;

/// <summary>
/// Класс-расширение для маппинга. Использует Mapster для автомаппинга.
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
        return statisticsRequest.Adapt<Statistics>();
    }
    
    /// <summary>
    /// Маппинг <see cref="Statistics"/> к <see cref="StatisticsResponse"/>
    /// </summary>
    /// <param name="statistics"><see cref="Statistics"/></param>
    /// <returns><see cref="StatisticsResponse"/></returns>
    public static StatisticsResponse MapToContract(this Statistics statistics)
    {
        return statistics.Adapt<StatisticsResponse>();
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