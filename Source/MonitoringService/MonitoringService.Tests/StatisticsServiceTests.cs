using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Services;
using MonitoringService.Contracts;
using Moq;

namespace MonitoringService.Tests;

public class StatisticsServiceTests
{
    private readonly Mock<IStatisticsRepository> _statisticsRepositoryMock;
    private readonly IStatisticsService _statisticsService;
    public StatisticsServiceTests()
    {
        _statisticsRepositoryMock = new Mock<IStatisticsRepository>();
        _statisticsService = new StatisticsService(_statisticsRepositoryMock.Object);
    }
    
    [Fact]
    public async Task AddStatisticsAsync_WithValidRequest_ShouldReturnId()
    {
        // Arrange
        int expectedId = 15;
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "DeviceName", OperatingSystem = "OS", Version = "1.0.1" };
        _statisticsRepositoryMock.Setup(repo => repo.AddStatAsync(It.IsAny<Statistics>())).ReturnsAsync(expectedId);

        // Act
        var result = await _statisticsService.AddStatisticsAsync(statisticsRequest);
        
        // Assert
        Assert.Equal(expectedId,result);
        _statisticsRepositoryMock.Verify(repo=>repo.AddStatAsync(It.IsAny<Statistics>()), Times.Once);
    }
    
    [Fact]
    public async Task AddStatisticsAsync_WithEmptyDeviceName_ShouldThrowBadRequestException()
    {
        // Arrange
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "", OperatingSystem = "OS", Version = "1.0.1" };

        // Act
        var func = async () => await _statisticsService.AddStatisticsAsync(statisticsRequest);
        
        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }
    
    [Fact]
    public async Task AddStatisticsAsync_WithEmptyOperatingSystem_ShouldThrowBadRequestException()
    {
        // Arrange
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "DeviceName", OperatingSystem = "", Version = "1.0.1" };

        // Act
        var func = async () => await _statisticsService.AddStatisticsAsync(statisticsRequest);
        
        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }
    
    [Fact]
    public async Task AddStatisticsAsync_WithEmptyVersion_ShouldThrowBadRequestException()
    {
        // Arrange
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "DeviceName", OperatingSystem = "OS", Version = "" };

        // Act
        var func = async () => await _statisticsService.AddStatisticsAsync(statisticsRequest);
        
        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
    }
    
    [Fact]
    public async Task DeleteStatisticsAsync_WithValidRequest_ShouldDeleteBook()
    {
        // Arrange
        int id = 1;
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(true);
        
        // Act
        await _statisticsService.DeleteStatisticsAsync(id);
        
        // Assert
        _statisticsRepositoryMock.Verify(repo => repo.DeleteStatAsync(id),Times.Once);
    }
    
    [Fact]
    public async Task DeleteStatisticsAsync_WithInvalidId_ShouldThrowStatisticsNotFoundException()
    {
        // Arrange
        int invalidId = 1;
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(invalidId)).ReturnsAsync(false);
        
        // Act
        var func = async () => await _statisticsService.DeleteStatisticsAsync(invalidId);
        
        // Assert
        await Assert.ThrowsAsync<StatisticsNotFoundException>(func);
    }
    
    [Fact]
    public async Task GetStatisticsAsync_WithValidRequest_ShouldReturnStatisticsResponse()
    {
        // Arrange
        int id = 15;
        DateTime currDateTime = DateTime.Now;
        var stat = new Statistics {Id = id,DeviceName = "DeviceName",OperatingSystem = "Os", Version = "1.0.1",LastUpdateDateTime = currDateTime};
        _statisticsRepositoryMock.Setup(repo => repo.GetStatAsync(id)).ReturnsAsync(stat);
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(stat.Id)).ReturnsAsync(true);

        // Act
        var result = await _statisticsService.GetStatisticsAsync(id);
        
        // Assert
        Assert.Equal(stat.Id,result.Id);
        Assert.Equal(stat.DeviceName,result.DeviceName);
        Assert.Equal(stat.OperatingSystem,result.OperatingSystem);
        Assert.Equal(stat.Version,result.Version);
        Assert.Equal(stat.LastUpdateDateTime,result.LastUpdateDateTime);
        _statisticsRepositoryMock.Verify(repo=>repo.GetStatAsync(id), Times.Once);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(stat.Id),Times.Once);
    }

    [Fact]
    public async Task GetStatisticsAsync_WithInvalidId_ShouldThrowStatisticsNotFoundException()
    {
        // Arrange
        int id = 15;
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(false);
        
        // Act
        var func = async () =>await _statisticsService.GetStatisticsAsync(id);
        
        // Assert
        await Assert.ThrowsAsync<StatisticsNotFoundException>(func);
    }

    [Fact]
    public async Task GetAllStatisticsAsync_ShouldReturnIEnumerableOfStatisticsResponse()
    {
        // Arrange
        var stats = new List<Statistics>()
        {
            new Statistics
            {
                Id = 1, DeviceName = "DeviceName", OperatingSystem = "Os", Version = "1.0.1",
                LastUpdateDateTime = DateTime.Now
            },
            new Statistics
            {
                Id = 2, DeviceName = "DeviceName", OperatingSystem = "Os", Version = "1.0.1",
                LastUpdateDateTime = DateTime.Now
            }
        };
        _statisticsRepositoryMock.Setup(repo => repo.GetStatsAsync()).ReturnsAsync(stats);

        // Act
        var result = await _statisticsService.GetAllStatisticsAsync();

        // Assert
        Assert.Equal(stats.Count,result.Count());
        _statisticsRepositoryMock.Verify(repo => repo.GetStatsAsync(),Times.Once);
    }
    
    [Fact]
    public async Task UpdateStatisticsAsync_WithValidId()
    {
        // Arrange
        int id = 15;
        var statRequest = new StatisticsRequest {DeviceName = "DeviceName",OperatingSystem = "Os", Version = "1.0.1"};
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(true);
        

        // Act
        await _statisticsService.UpdateStatisticsAsync(id,statRequest);
        
        // Assert
        _statisticsRepositoryMock.Verify(repo=>repo.UpdateStatAsync(id,It.IsAny<Statistics>()), Times.Once);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(id),Times.Once);
    }
    
    [Fact]
    public async Task UpdateStatisticsAsync_WithInvalidId_ShouldThrowStatisticsNotFoundException()
    {
        // Arrange
        int id = 15;
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(false);
        
        // Act
        var func = async () =>await _statisticsService.UpdateStatisticsAsync(id,new StatisticsRequest());
        
        // Assert
        await Assert.ThrowsAsync<StatisticsNotFoundException>(func);
    }
    
    [Fact]
    public async Task UpdateStatisticsAsync_WithEmptyDeviceNameShouldThrowBadRequestException()
    {
        // Arrange
        int id = 15;
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "", OperatingSystem = "OS", Version = "1.0.1" };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(true);
        
        // Act
        var func = async () => await _statisticsService.UpdateStatisticsAsync(id,statisticsRequest);
        
        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(id),Times.Once);
    }
    
    [Fact]
    public async Task UpdateStatisticsAsync_WithEmptyOperatingSystemShouldThrowBadRequestException()
    {
        // Arrange
        int id = 15;
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "DeviceName", OperatingSystem = "", Version = "1.0.1" };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(true);
        
        // Act
        var func = async () => await _statisticsService.UpdateStatisticsAsync(id,statisticsRequest);
        
        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(id),Times.Once);
    }
    
    [Fact]
    public async Task UpdateStatisticsAsync_WithEmptyVersionShouldThrowBadRequestException()
    {
        // Arrange
        int id = 15;
        var statisticsRequest = new StatisticsRequest
            { DeviceName = "DeviceName", OperatingSystem = "OS", Version = "" };
        _statisticsRepositoryMock.Setup(repo => repo.StatExistsAsync(id)).ReturnsAsync(true);
        
        // Act
        var func = async () => await _statisticsService.UpdateStatisticsAsync(id,statisticsRequest);
        
        // Assert
        await Assert.ThrowsAsync<BadRequestException>(func);
        _statisticsRepositoryMock.Verify(repo => repo.StatExistsAsync(id),Times.Once);
    }
}