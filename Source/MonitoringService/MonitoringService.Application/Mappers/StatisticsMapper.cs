using MonitoringService.Application.Models;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Mappers;

public static class StatisticsMapper
{
    public static Statistics MapToDomain(this StatisticsRequest statisticsRequest)
    {
        return new Statistics()
        {
            DeviceName = statisticsRequest.DeviceData.DeviceName,
            OperatingSystem = statisticsRequest.DeviceData.OperatingSystem,
            Version = statisticsRequest.AppData.Version,
            LastUpdateDateTime = DateTime.Now
        };
    }

    public static StatisticsResponse MapToContract(this Statistics statistics)
    {
        return new StatisticsResponse()
        {
            Id = statistics.Id,
            AppData = new Contracts.AppData()
            {
                Version = statistics.Version
            },
            DeviceData = new Contracts.DeviceData()
            {
                DeviceName = statistics.DeviceName,
                OperatingSystem = statistics.OperatingSystem
            },
            LastUpdateDateTime = statistics.LastUpdateDateTime
        };
    }

    public static IEnumerable<StatisticsResponse> MapToContract(this IEnumerable<Statistics> statistics)
    {
        return statistics.Select(x => x.MapToContract());
    }
}