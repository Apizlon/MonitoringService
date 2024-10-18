using MonitoringService.Application.Models;

namespace MonitoringService.Contracts;

public class StatisticsRequest
{
    public DeviceData DeviceData { get; set; }
    public AppData AppData { get; set; }
}