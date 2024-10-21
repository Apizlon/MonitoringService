using Microsoft.EntityFrameworkCore;
using MonitoringService.Application.Models;

namespace MonitoringService.Application.Repositories;

/// <summary>
/// Реализация интерфейса <see cref="IStatisticsRepository"/>>
/// </summary>
public class StatisticsRepository : IStatisticsRepository
{
    /// <summary>
    /// <see cref="StatisticsDbContext"/>>
    /// </summary>
    private readonly StatisticsDbContext _context;
    
    public StatisticsRepository(StatisticsDbContext context)
    {
        _context = context;
    }

    public async Task<ulong> AddStatAsync(Statistics statistics)
    {
        _context.Stats.Add(statistics);
        await _context.SaveChangesAsync();
        return statistics.Id;
    }

    public async Task DeleteStatAsync(ulong id)
    {
        var stat = await _context.Stats.FindAsync(id);
        _context.Stats.Remove(stat);
        await _context.SaveChangesAsync();
    }

    public async Task<Statistics> GetStatAsync(ulong id)
    {
        return await _context.Stats.FindAsync(id);
    }
    
    public async Task<IEnumerable<Statistics>> GetStatsAsync()
    {
        return await _context.Stats.ToListAsync();
    }

    public async Task UpdateStatAsync(ulong id, Statistics statistics)
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

    public async Task<bool> StatExistsAsync(ulong id)
    {
        var stat = await _context.Stats.FindAsync(id);
        return stat!=null;
    }
}