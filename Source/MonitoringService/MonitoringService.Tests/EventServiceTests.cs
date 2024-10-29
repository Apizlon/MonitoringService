using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Services;
using MonitoringService.Contracts;
using Moq;

namespace MonitoringService.Tests;

public class EventServiceTests
{
    private readonly Mock<IStatisticsRepository> _statisticsRepositoryMock;
    private readonly Mock<IEventRepository> _eventRepositoryMock;
    private readonly IEventService _eventService;

    public EventServiceTests()
    {
        _statisticsRepositoryMock = new Mock<IStatisticsRepository>();
        _eventRepositoryMock = new Mock<IEventRepository>();
        _eventService = new EventService(_eventRepositoryMock.Object,_statisticsRepositoryMock.Object);
    }

    [Fact]
    public async Task AddEventAsync_WithValidRequest_ShouldReturnId()
    {
        // Arrange
        Guid expectedId = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.AddEventAsync(It.IsAny<StatEvent>())).ReturnsAsync(expectedId);

        // Act
        var result = await _eventService.AddEventAsync(eventRequest);

        // Assert
        Assert.Equal(expectedId,result);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
        _eventRepositoryMock.Verify(repo => repo.AddEventAsync(It.IsAny<StatEvent>()),Times.Once);
    }
    
    [Fact]
    public async Task AddEventAsync_WithInvalidId_ShouldThrowStatisticsNotFoundException()
    {
        // Arrange
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(false);
        

        // Act
        var func = async () => await _eventService.AddEventAsync(eventRequest);

        // Assert
        await Assert.ThrowsAsync<StatisticsNotFoundException>(func);
    }
    
    [Fact]
    public async Task AddEventAsync_WithEmptyDescription_ShouldThrowBadRequestException()
    {
        // Arrange
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "", Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.AddEventAsync(eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task AddEventAsync_WithDescriptionMoreThan1024Symbols_ShouldThrowBadRequestException()
    {
        // Arrange
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = new string('A',1500), Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.AddEventAsync(eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task AddEventAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.AddEventAsync(eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task AddEventAsync_WithNameMoreThan50Symbols_ShouldThrowBadRequestException()
    {
        // Arrange
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = new string('A',100), EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.AddEventAsync(eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task DeleteEventAsync_WithValidId()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);

        // Act
        await _eventService.DeleteEventAsync(id);

        // Assert
        _eventRepositoryMock.Verify(repo => repo.DeleteEventAsync(id),Times.Once);
    }
    
    [Fact]
    public async Task DeleteEventAsync_WithInvalidId_ShouldThrowEventNotFoundException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(false);

        // Act
        var func = async () => await _eventService.DeleteEventAsync(id);

        // Assert
        await Assert.ThrowsAsync<EventNotFoundException>(func);
    }
    
    [Fact]
    public async Task GetEventAsync_WithValidId_ShouldReturnEventResponse()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var statEvent = new StatEvent
            { Id = id,StatisticsId = 1, Description = "Description", Name = "Name", EventDateTime = DateTime.Now };
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(statEvent.Id)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.GetEventAsync(id)).ReturnsAsync(statEvent);

        // Act
        var result = await _eventService.GetEventAsync(id);

        // Assert
        Assert.Equal(statEvent.Id,result.Id);
        Assert.Equal(statEvent.StatisticsId,result.StatisticsId);
        Assert.Equal(statEvent.Description,result.Description);
        Assert.Equal(statEvent.Name ,result.Name );
        Assert.Equal(statEvent.EventDateTime,result.EventDateTime);
        _eventRepositoryMock.Verify(repo => repo.EventExistsAsync(statEvent.Id),Times.Once);
        _eventRepositoryMock.Verify(repo => repo.GetEventAsync(id),Times.Once);
    }
    
    [Fact]
    public async Task GetEventAsync_WithInvalidId_ShouldThrowEventNotFoundException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(false);

        // Act
        var func = async () => await _eventService.GetEventAsync(id);

        // Assert
        await Assert.ThrowsAsync<EventNotFoundException>(func);
    }

    [Fact]
    public async Task GetEventsByStatisticsIdAsync_WithValidId_ShouldReturnIEnumerableOfEventResponse()
    {
        // Arrange
        int statId = 15;
        var events = new List<StatEvent>()
        {
            new StatEvent
            {
                Id = Guid.NewGuid(), StatisticsId = statId, Description = "Description", Name = "Name",
                EventDateTime = DateTime.Now
            },
            new StatEvent
            {
                Id = Guid.NewGuid(), StatisticsId = statId, Description = "Description", Name = "Name",
                EventDateTime = DateTime.Now
            }
        };
        
        _statisticsRepositoryMock.Setup((repo => repo.StatExistsAsync(statId))).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.GetEventsByStatisticsIdAsync(statId)).ReturnsAsync(events);
        
        // Act
        var result = await _eventService.GetEventsByStatisticsIdAsync(statId);

        // Assert
        Assert.Equal(events.Count,result.Count());
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(statId),Times.Once);
        _eventRepositoryMock.Verify(repo => repo.GetEventsByStatisticsIdAsync(statId),Times.Once);
    }
    
    [Fact]
    public async Task GetEventsByStatisticsIdAsync_WithInvalidId_ShouldThrowStatisticsNotFoundException()
    {
        // Arrange
        int statId = 15;
        _statisticsRepositoryMock.Setup((repo => repo.StatExistsAsync(statId))).ReturnsAsync(false);
        
        // Act
        var func = async () => await _eventService.GetEventsByStatisticsIdAsync(statId);

        // Assert
        await Assert.ThrowsAsync<StatisticsNotFoundException>(func);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithValidRequest()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);
        
        // Act
        await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        _eventRepositoryMock.Verify(repo => repo.EventExistsAsync(id),Times.Once);
        _eventRepositoryMock.Verify(repo => repo.UpdateEventAsync(id,It.IsAny<StatEvent>()),Times.Once);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithInvalidStatisticsId_ShouldThrowStatisticsNotFoundException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "Name", EventDateTime = DateTime.Now };
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(false);

        // Act
        var func = async () => await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        await Assert.ThrowsAsync<StatisticsNotFoundException>(func);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithInvalidEventId_ShouldThrowEventNotFoundException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "Name", EventDateTime = DateTime.Now };
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(false);

        // Act
        var func = async () => await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        await Assert.ThrowsAsync<EventNotFoundException>(func);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithEmptyDescription_ShouldThrowBadRequestException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "", Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithDescriptionMoreThan1024Symbols_ShouldThrowBadRequestException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = new string('A',1500), Name = "Name", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithEmptyName_ShouldThrowBadRequestException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = "", EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
    
    [Fact]
    public async Task UpdateEventAsync_WithNameMoreThan50Symbols_ShouldThrowBadRequestException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var eventRequest = new EventRequest
            { StatisticsId = 1, Description = "Description", Name = new string('A',100), EventDateTime = DateTime.Now };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(eventRequest.StatisticsId)).ReturnsAsync(true);
        _eventRepositoryMock.Setup(repo => repo.EventExistsAsync(id)).ReturnsAsync(true);

        // Act
        var func = async () => await _eventService.UpdateEventAsync(id,eventRequest);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(eventRequest.StatisticsId),Times.Once);
    }
}