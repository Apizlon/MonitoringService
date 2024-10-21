using MonitoringService.Contracts;

namespace MonitoringService.Application.Models;

public class StatisticsResponse
{
    public ulong Id { get; set; }
    public string DeviceName { get; set; }
    public string OperatingSystem { get; set; }
    public string Version { get; set; }
    public DateTime LastUpdateDateTime { get; set; }
}