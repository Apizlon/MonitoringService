using Microsoft.AspNetCore.Mvc;
using MonitoringService.Application.Processors;
using MonitoringService.Contracts;

namespace MonitoringService.Host.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ControllerBase
{
    private readonly EventProcessor _eventProcessor;
    private readonly ILogger<EventController> _logger;

    public EventController(EventProcessor eventProcessor,ILogger<EventController> logger)
    {
        _eventProcessor = eventProcessor;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent([FromBody] EventRequest eventRequest)
    {
        _logger.LogInformation("Запрос на добавления события");
        var id = await _eventProcessor.AddEventAsync(eventRequest);
        _logger.LogInformation($"Событие добавлено, его id {id}");
        return Ok(id);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEvent(Guid id)
    {
        _logger.LogInformation($"Запрос на получение события с id {id}");
        var statEvent = await _eventProcessor.GetEventAsync(id);
        _logger.LogInformation("Событие успешно получено");
        return Ok(statEvent);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEvent(Guid id,[FromBody] EventRequest eventRequest)
    {
        _logger.LogInformation($"Запрос на изменение события с id {id}");
        await _eventProcessor.UpdateEventAsync(id, eventRequest);
        _logger.LogInformation("Событие успешно изменено");
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        _logger.LogInformation($"Запрос на удаление события с id {id}");
        await _eventProcessor.DeleteEventAsync(id);
        _logger.LogInformation("Событие успешно удалено");
        return Ok();
    }
}