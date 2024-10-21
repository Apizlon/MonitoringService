namespace MonitoringService.Contracts;

/// <summary>
/// DTO для статистики(запрос)>> 
/// </summary>
public class StatisticsRequest
{
    /// <summary>
    /// Имя устройства
    /// </summary>
    public string DeviceName { get; set; }
    
    /// <summary>
    /// Операционная система на устройстве
    /// </summary>
    public string OperatingSystem { get; set; }
    
    /// <summary>
    /// Версия установленного приложения
    /// </summary>
    public string Version { get; set; }
}