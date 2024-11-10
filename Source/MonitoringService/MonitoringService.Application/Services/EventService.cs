using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Mappers;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Validator;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

/// <inheritdoc />
public class EventService : IEventService
{
    /// <see cref="UnitOfWork"/>
    private readonly IUnitOfWork _unitOfWork;
    
    /// <summary>
    /// Конструктор с 1 параметром
    /// </summary>
    /// <param name="unitOfWork"><see cref="UnitOfWork"/></param>
    public EventService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <inheritdoc />
    public async Task<Guid> AddEventAsync(EventRequest eventRequest)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            
            bool statExists = await _unitOfWork._StatisticsRepository.StatExistsAsync(eventRequest.StatisticsId);
            if (!statExists)
            {
                throw new StatisticsNotFoundException(eventRequest.StatisticsId);
            }

            eventRequest.Validate();
            StatEvent statEvent = eventRequest.MapToDomain();
            statEvent.Id = Guid.NewGuid();
            await _unitOfWork._StatisticsRepository.UpdateStatLastUpdateDateTimeAsync(statEvent.StatisticsId,
                statEvent.EventDateTime);
            Guid addedEventId = await _unitOfWork._EventRepository.AddEventAsync(statEvent);
            
            _unitOfWork.Commit();
            return addedEventId;
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
    
    /// <inheritdoc />
    public async Task DeleteEventAsync(Guid id)
    {
        bool eventExists = await _unitOfWork._EventRepository.EventExistsAsync(id);
        if (!eventExists)
        {
            throw new EventNotFoundException(id);
        }

        await _unitOfWork._EventRepository.DeleteEventAsync(id);
    }
    
    /// <inheritdoc />
    public async Task<EventResponse> GetEventAsync(Guid id)
    {
        bool eventExists = await _unitOfWork._EventRepository.EventExistsAsync(id);
        if (!eventExists)
        {
            throw new EventNotFoundException(id);
        }

        var eventResponse = await _unitOfWork._EventRepository.GetEventAsync(id);
        return eventResponse.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<EventResponse>> GetEventsByStatisticsIdAsync(int statisticsId)
    {
        bool statExists = await _unitOfWork._StatisticsRepository.StatExistsAsync(statisticsId);
        if (!statExists)
        {
            throw new StatisticsNotFoundException(statisticsId);
        }

        var events = await _unitOfWork._EventRepository.GetEventsByStatisticsIdAsync(statisticsId);
        return events.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task UpdateEventAsync(Guid id, EventRequest eventRequest)
    {
        try
        {
            _unitOfWork.BeginTransaction();

            bool eventExists = await _unitOfWork._EventRepository.EventExistsAsync(id);
            if (!eventExists)
            {
                throw new EventNotFoundException(id);
            }

            bool statExists = await _unitOfWork._StatisticsRepository.StatExistsAsync(eventRequest.StatisticsId);
            if (!statExists)
            {
                throw new StatisticsNotFoundException(eventRequest.StatisticsId);
            }

            eventRequest.Validate();
            await _unitOfWork._EventRepository.UpdateEventAsync(id,eventRequest.MapToDomain());
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }
}