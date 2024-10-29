using MonitoringService.Application.Exceptions;
using MonitoringService.Application.Mappers;
using MonitoringService.Application.Models;
using MonitoringService.Application.Repositories;
using MonitoringService.Application.Validator;
using MonitoringService.Contracts;

namespace MonitoringService.Application.Services;

/// <inheritdoc />
public class StatisticsService : IStatisticsService
{
    private readonly IUnitOfWork _unitOfWork;
    
    public StatisticsService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    /// <inheritdoc />
    public async Task<int> AddStatisticsAsync(StatisticsRequest statisticsRequest)
    {
        statisticsRequest.Validate();
        Statistics statistics = statisticsRequest.MapToDomain();
        statistics.LastUpdateDateTime=DateTime.Now;
        return await _unitOfWork._StatisticsRepository.AddStatAsync(statistics);
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<StatisticsResponse>> GetAllStatisticsAsync()
    {
        var allStatistics = await _unitOfWork._StatisticsRepository.GetStatsAsync();
        return allStatistics.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task<StatisticsResponse> GetStatisticsAsync(int id)
    {
        var isStatExists = await _unitOfWork._StatisticsRepository.StatExistsAsync(id);
        if (!isStatExists)
        {
            throw new StatisticsNotFoundException(id);
        }
        var statistics = await _unitOfWork._StatisticsRepository.GetStatAsync(id);
        return statistics.MapToContract();
    }
    
    /// <inheritdoc />
    public async Task UpdateStatisticsAsync(int id, StatisticsRequest statisticsRequest)
    {
        var isStatisticsExists = await _unitOfWork._StatisticsRepository.StatExistsAsync(id);
        if (!isStatisticsExists)
        {
            throw new StatisticsNotFoundException(id);
        }
        statisticsRequest.Validate();
        Statistics statistics = statisticsRequest.MapToDomain();
        statistics.LastUpdateDateTime = DateTime.Now;;
        await _unitOfWork._StatisticsRepository.UpdateStatAsync(id, statistics);
    }
    
    /// <inheritdoc />
    public async Task DeleteStatisticsAsync(int id)
    {
        var isStatisticsExists = await _unitOfWork._StatisticsRepository.StatExistsAsync(id);
        if (!isStatisticsExists)
        {
            throw new StatisticsNotFoundException(id);
        }

        await _unitOfWork._StatisticsRepository.DeleteStatAsync(id);
    }
}