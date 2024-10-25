namespace MonitoringService.Contracts;

public class EventRequest
{
    public int StatisticsId { get; set; }
    public DateTime EventDateTime { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}