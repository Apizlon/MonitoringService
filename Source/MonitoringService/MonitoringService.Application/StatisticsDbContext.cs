using Microsoft.EntityFrameworkCore;
using MonitoringService.Application.Models;

namespace MonitoringService.Application;

/// <summary>
/// Контекст базы данных
/// </summary>
public class StatisticsDbContext : DbContext
{
    /// <summary>
    /// Конфигурация параметров
    /// </summary>
    /// <param name="optionsBuilder"><see cref="DbContextOptionsBuilder"/>></param>
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "StatisticsDb");
    }
    
    /// <summary>
    /// Коллекция сущностей типа <see cref="Statistics"/>>
    /// </summary>
    public DbSet<Statistics> Stats { get; set; }
}