using Microsoft.AspNetCore.Mvc;
using MonitoringService.Application.Processors;
using MonitoringService.Contracts;

namespace MonitoringService.Host.Controllers;

/// <summary>
/// Api для получения информации о событиях
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    /// <see cref="EventProcessor"/>
    private readonly EventProcessor _eventProcessor;
    
    /// <summary>
    /// Логгер
    /// </summary>
    private readonly ILogger<EventController> _logger;
    
    /// <summary>
    /// Конструктор с двумя параметрами
    /// </summary>
    /// <param name="eventProcessor"><see cref="EventProcessor"/></param>
    /// <param name="logger">Логгер</param>
    public EventController(EventProcessor eventProcessor,ILogger<EventController> logger)
    {
        _eventProcessor = eventProcessor;
        _logger = logger;
    }
    
    /// <summary>
    /// Endpoint добавления события
    /// </summary>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    /// <returns>id добавленного события</returns>
    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] EventRequest eventRequest)
    {
        _logger.LogInformation("Запрос на добавления события");
        var id = await _eventProcessor.AddEventAsync(eventRequest);
        _logger.LogInformation($"Событие добавлено, его id {id}");
        return Ok(id);
    }
    
    /// <summary>
    /// Endpoint получения события по id
    /// </summary>
    /// <param name="id">id события</param>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        _logger.LogInformation($"Запрос на получение события с id {id}");
        var statEvent = await _eventProcessor.GetEventAsync(id);
        _logger.LogInformation("Событие успешно получено");
        return Ok(statEvent);
    }
    
    /// <summary>
    /// Endpoint обновления события
    /// </summary>
    /// <param name="id">id события</param>
    /// <param name="eventRequest"><see cref="EventRequest"/></param>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEvent(Guid id,[FromBody] EventRequest eventRequest)
    {
        _logger.LogInformation($"Запрос на изменение события с id {id}");
        await _eventProcessor.UpdateEventAsync(id, eventRequest);
        _logger.LogInformation("Событие успешно изменено");
        return Ok();
    }

    /// <summary>
    /// Endpoint Удаления события
    /// </summary>
    /// <param name="id">id удаляемого объекта</param>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        _logger.LogInformation($"Запрос на удаление события с id {id}");
        await _eventProcessor.DeleteEventAsync(id);
        _logger.LogInformation("Событие успешно удалено");
        return Ok();
    }
}