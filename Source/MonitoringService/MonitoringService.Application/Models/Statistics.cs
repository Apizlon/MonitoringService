using System.ComponentModel.DataAnnotations;

namespace MonitoringService.Application.Models;

/// <summary>
/// Класс статистики для сбора
/// </summary>
public class Statistics
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
    
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
    
    /// <summary>
    /// Дата получения последней статистики
    /// </summary>
    public DateTime LastUpdateDateTime { get; set; }
}