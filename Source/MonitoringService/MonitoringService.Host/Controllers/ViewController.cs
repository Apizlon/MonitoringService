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
    /// <summary>
    /// <see cref="StatisticsProcessor"/>
    /// </summary>
    private readonly StatisticsProcessor _statisticsProcessor;
    
    /// <summary>
    /// Логгер
    /// </summary>
    private readonly ILogger<ViewController> _logger;
    
    /// <summary>
    /// Конструктор контроллера с 2 параметрами
    /// </summary>
    /// <param name="statisticsProcessor"><see cref="StatisticsProcessor"/></param>
    /// <param name="logger">Логгер</param>
    public ViewController(StatisticsProcessor statisticsProcessor,ILogger<ViewController> logger)
    {
        _statisticsProcessor = statisticsProcessor;
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
}