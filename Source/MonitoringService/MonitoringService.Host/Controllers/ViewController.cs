using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MonitoringService.Application.Processors;
using MonitoringService.Contracts;

namespace MonitoringService.Host.Controllers;

/// <summary>
/// API для отображения всех устройств
/// </summary>
[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAllOrigins")]
public class ViewController : ControllerBase
{
    /// <see cref="StatisticsProcessor"/>
    private readonly StatisticsProcessor _statisticsProcessor;
    
    /// <see cref="EventProcessor"/>
    private readonly EventProcessor _eventProcessor;
    
    /// <summary>
    /// Логгер
    /// </summary>
    private readonly ILogger<ViewController> _logger;
    
    /// <summary>
    /// Конструктор контроллера с 3 параметрами
    /// </summary>
    /// <param name="statisticsProcessor"><see cref="StatisticsProcessor"/></param>
    /// <param name="eventProcessor"><see cref="EventProcessor"/></param>
    /// <param name="logger">Логгер</param>
    public ViewController(StatisticsProcessor statisticsProcessor,EventProcessor eventProcessor,ILogger<ViewController> logger)
    {
        _statisticsProcessor = statisticsProcessor;
        _eventProcessor = eventProcessor;
        _logger = logger;
    }
    
    /// <summary>
    /// Endpoint для получения всех устройств
    /// </summary>
    /// <returns>IEnumerable объектов типа <see cref="StatisticsResponse"/></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllStatistics()
    {
        _logger.LogInformation("Запрос на получение статистики");
        var allStatistics = await _statisticsProcessor.GetAllStatisticsAsync();
        _logger.LogInformation("Статистика успешно получена");
        return Ok(allStatistics);
    }
    
    /// <summary>
    /// Endpoint для получения всех событий для конкретного устройства(статистики)
    /// </summary>
    /// <param name="id">id устройства(статистики)</param>
    /// <returns>IEnumerable объектов типа <see cref="EventResponse"/></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetEventsByStatisticsId(int id)
    {
        _logger.LogInformation($"Запрос на получение событий устройства с id {id}");
        var events = await _eventProcessor.GetEventsByStatisticsIdAsync(id);
        _logger.LogInformation($"События устройства с id {id} успешно получены");
        return Ok(events);
    }
}