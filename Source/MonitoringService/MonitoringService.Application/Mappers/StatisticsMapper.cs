using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Mappers;

public static class StatisticsMapper
{
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

    public static IEnumerable<StatisticsResponse> MapToContract(this IEnumerable<Statistics> statistics)
    {
        return statistics.Select(x => x.MapToContract());
    }
}