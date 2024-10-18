using Microsoft.AspNetCore.Mvc;
using MonitoringService.Application.Processors;
using MonitoringService.Contracts;

namespace MonitoringService.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly StatisticsProcessor _statisticsProcessor;
    private readonly ILogger<StatisticsController> _logger;

    public StatisticsController(StatisticsProcessor statisticsProcessor,ILogger<StatisticsController> logger)
    {
        _statisticsProcessor = statisticsProcessor;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddStatistics([FromBody] StatisticsRequest statisticsRequest)
    {
        _logger.LogInformation("Запрос на добавление статистики устройства");
        var id = await _statisticsProcessor.AddStatisticsAsync(statisticsRequest);
        _logger.LogInformation($"Устройство с именем {statisticsRequest.DeviceData.DeviceName} успешно добавлено, его id {id}.");
        return Ok(id);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetStatistics(ulong id)
    {
        _logger.LogInformation($"Запрос на получение статистики устройства с id {id}.");
        var statistics = await _statisticsProcessor.GetStatisticsAsync(id);
        _logger.LogInformation($"Статистика устройства с id {id} получена.");
        return Ok(statistics);
    }
    
    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateStatistics(ulong id,[FromBody] StatisticsRequest statisticsRequest)
    {
        _logger.LogInformation($"Запрос на обновление статистики устройства с id {id}.");
        await _statisticsProcessor.UpdateStatisticsAsync(id,statisticsRequest);
        _logger.LogInformation($"Статистика устройства с id {id} обновлена.");
        return Ok();
    }
    
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteStatistics(ulong id)
    {
        _logger.LogInformation($"Запрос на удаление статистики устройства с id {id}.");
        await _statisticsProcessor.DeleteStatisticsAsync(id);
        _logger.LogInformation($"Статистика устройства с id {id} удалена.");
        return Ok();
    }
}