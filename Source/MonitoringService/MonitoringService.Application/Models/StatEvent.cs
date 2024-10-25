namespace MonitoringService.Application.Models;

public class StatEvent
{
    public Guid Id { get; set; }
    public int StatisticsId { get; set; }
    public DateTime EventDateTime { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}