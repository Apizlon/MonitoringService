using MonitoringService.Application.Models;

namespace MonitoringService.Contracts;

public class StatisticsRequest
{
    public string DeviceName { get; set; }
    public string OperatingSystem { get; set; }
    public string Version { get; set; }
}