using Microsoft.EntityFrameworkCore;
using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

/// <summary>
/// Старая реализация. Использует in-memory базу данных и EF.
/// </summary>
public class StatisticsRepository_EF : IStatisticsRepository
{
    /// <summary>
    /// <see cref="StatisticsDbContext"/>
    /// </summary>
    private readonly StatisticsDbContext _context;
    
    /// <summary>
    /// Конструктор с одним параметром
    /// </summary>
    /// <param name="context"><see cref="StatisticsDbContext"/></param>
    public StatisticsRepository_EF(StatisticsDbContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc />
    public async Task<int> AddStatAsync(Statistics statistics)
    {
        _context.Stats.Add(statistics);
        await _context.SaveChangesAsync();
        return statistics.Id;
    }
    
    /// <inheritdoc />
    public async Task DeleteStatAsync(int id)
    {
        var stat = await _context.Stats.FindAsync(id);
        _context.Stats.Remove(stat);
        await _context.SaveChangesAsync();
    }
    
    /// <inheritdoc />
    public async Task<Statistics> GetStatAsync(int id)
    {
        return await _context.Stats.FindAsync(id);
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<Statistics>> GetStatsAsync()
    {
        return await _context.Stats.ToListAsync();
    }
    
    /// <inheritdoc />
    public async Task UpdateStatAsync(int id, Statistics statistics)
    {
        var existingStat = await _context.Stats.FindAsync(id);
        if (existingStat != null)
        {
            existingStat.DeviceName = statistics.DeviceName;
            existingStat.OperatingSystem = statistics.OperatingSystem;
            existingStat.Version = statistics.Version;
            existingStat.LastUpdateDateTime = statistics.LastUpdateDateTime;
            await _context.SaveChangesAsync();
        }
    }
    
    /// <inheritdoc />
    public async Task<bool> StatExistsAsync(int id)
    {
        var stat = await _context.Stats.FindAsync(id);
        return stat!=null;
    }
}