using Microsoft.AspNetCore.Mvc;
using MonitoringService.Application.Processors;
using MonitoringService.Contracts;

namespace MonitoringService.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ViewController : ControllerBase
{
    private readonly StatisticsProcessor _statisticsProcessor;
    private readonly ILogger<ViewController> _logger;

    public ViewController(StatisticsProcessor statisticsProcessor,ILogger<ViewController> logger)
    {
        _statisticsProcessor = statisticsProcessor;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStatistics()
    {
        _logger.LogInformation("Запрос на получение статистики");
        var allStatistics = await _statisticsProcessor.GetAllStatisticsAsync();
        _logger.LogInformation("Статистика успешно получена");
        return Ok(allStatistics);
    }
}