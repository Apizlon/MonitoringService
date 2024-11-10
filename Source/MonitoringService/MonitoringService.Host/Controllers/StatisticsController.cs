using Microsoft.AspNetCore.Mvc;
using MonitoringService.Application.Processors;
using MonitoringService.Contracts;

namespace MonitoringService.Host.Controllers;

/// <summary>
/// Api для получения статистической информации от устройств
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    /// <see cref="StatisticsProcessor"/>>
    private readonly StatisticsProcessor _statisticsProcessor;
    
    /// <summary>
    /// Логгер
    /// </summary>
    private readonly ILogger<StatisticsController> _logger;
    
    /// <summary>
    /// Конструктор с двумя параметрами
    /// </summary>
    /// <param name="statisticsProcessor"><see cref="StatisticsProcessor"/></param>
    /// <param name="logger">Логгер</param>
    public StatisticsController(StatisticsProcessor statisticsProcessor,ILogger<StatisticsController> logger)
    {
        _statisticsProcessor = statisticsProcessor;
        _logger = logger;
    }
    
    /// <summary>
    /// Endpoint добавления статистики
    /// </summary>
    /// <param name="statisticsRequest"><see cref="StatisticsRequest"/>></param>
    /// <returns>id добавленного соткудника</returns>
    [HttpPost]
    public async Task<IActionResult> AddStatistics([FromBody] StatisticsRequest statisticsRequest)
    {
        _logger.LogInformation("Запрос на добавление статистики устройства");
        var id = await _statisticsProcessor.AddStatisticsAsync(statisticsRequest);
        _logger.LogInformation($"Устройство с именем {statisticsRequest.DeviceName} успешно добавлено, его id {id}.");
        return Ok(id);
    }
    
    /// <summary>
    /// Endpoint получения статистики
    /// </summary>
    /// <param name="id">id</param>
    /// <returns><see cref="StatisticsResponse"/></returns>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStatistics(int id)
    {
        _logger.LogInformation($"Запрос на получение статистики устройства с id {id}.");
        var statistics = await _statisticsProcessor.GetStatisticsAsync(id);
        _logger.LogInformation($"Статистика устройства с id {id} получена.");
        return Ok(statistics);
    }
    
    /// <summary>
    /// Endpoint обновления статистики
    /// </summary>
    /// <param name="id">id обновляемого элемента</param>
    /// <param name="statisticsRequest"><see cref="StatisticsRequest"/></param>
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStatistics(int id,[FromBody] StatisticsRequest statisticsRequest)
    {
        _logger.LogInformation($"Запрос на обновление статистики устройства с id {id}.");
        await _statisticsProcessor.UpdateStatisticsAsync(id,statisticsRequest);
        _logger.LogInformation($"Статистика устройства с id {id} обновлена.");
        return Ok();
    }
    
    /// <summary>
    /// Endpoint удаления статистики
    /// </summary>
    /// <param name="id">id удаляемого элемента</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStatistics(int id)
    {
        _logger.LogInformation($"Запрос на удаление статистики устройства с id {id}.");
        await _statisticsProcessor.DeleteStatisticsAsync(id);
        _logger.LogInformation($"Статистика устройства с id {id} удалена.");
        return Ok();
    }
}