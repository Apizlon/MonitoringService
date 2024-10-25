using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Mappers;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Validator;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IStatisticsRepository _statisticsRepository;
    
    public EventService(IEventRepository eventRepository, IStatisticsRepository statisticsRepository)
    {
        _eventRepository = eventRepository;
        _statisticsRepository = statisticsRepository;
    }

    public async Task<Guid> AddEventAsync(EventRequest eventRequest)
    {
        bool statExists = await _statisticsRepository.StatExistsAsync(eventRequest.StatisticsId);
        if (!statExists)
        {
            throw new StatisticsNotFoundException(eventRequest.StatisticsId);
        }
        eventRequest.Validate();
        StatEvent statEvent = eventRequest.MapToDomain();
        statEvent.Id = Guid.NewGuid();
        return await _eventRepository.AddEventAsync(statEvent);
    }

    public async Task DeleteEventAsync(Guid id)
    {
        bool eventExists = await _eventRepository.EventExistsAsync(id);
        if (!eventExists)
        {
            throw new EventNotFoundException(id);
        }

        await _eventRepository.DeleteEventAsync(id);
    }

    public async Task<EventResponse> GetEventAsync(Guid id)
    {
        bool eventExists = await _eventRepository.EventExistsAsync(id);
        if (!eventExists)
        {
            throw new EventNotFoundException(id);
        }

        var eventResponse = await _eventRepository.GetEventAsync(id);
        return eventResponse.MapToContract();
    }

    public async Task<IEnumerable<EventResponse>> GetEventsByStatisticsIdAsync(int statisticsId)
    {
        bool statExists = await _statisticsRepository.StatExistsAsync(statisticsId);
        if (!statExists)
        {
            throw new StatisticsNotFoundException(statisticsId);
        }

        var events = await _eventRepository.GetEventsByStatisticsIdAsync(statisticsId);
        return events.MapToContract();
    }

    public async Task UpdateEventAsync(Guid id, EventRequest eventRequest)
    {
        bool eventExists = await _eventRepository.EventExistsAsync(id);
        if (!eventExists)
        {
            throw new EventNotFoundException(id);
        }

        await _eventRepository.UpdateEventAsync(id,eventRequest.MapToDomain());
    }
}