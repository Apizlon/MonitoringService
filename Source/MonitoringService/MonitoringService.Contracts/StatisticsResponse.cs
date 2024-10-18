using MonitoringService.Contracts;

namespace MonitoringService.Application.Models;

public class StatisticsResponse
{
    public ulong Id { get; set; }
    public DeviceData DeviceData { get; set; }
    public AppData AppData { get; set; }
    public DateTime LastUpdateDateTime { get; set; }
}