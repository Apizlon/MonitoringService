namespace MonitoringService.Contracts;

/// <summary>
/// DTO для события(запрос)
/// </summary>
public class EventRequest
{
    /// <summary>
    /// ID статистики на которой произошло событие
    /// </summary>
    public int StatisticsId { get; set; }
    
    /// <summary>
    /// Дата и время события
    /// </summary>
    public DateTime EventDateTime { get; set; }
    
    /// <summary>
    /// Наименование события (до 50 символов)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Описание (до 1024 символов)
    /// </summary>
    public string Description { get; set; }
}