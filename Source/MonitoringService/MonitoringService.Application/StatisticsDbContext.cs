using Microsoft.EntityFrameworkCore;
using MonitoringService.Application.Models;

namespace MonitoringService.Application;

public class StatisticsDbContext : DbContext
{
    protected override void OnConfiguring
        (DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "StatisticsDb");
    }
    
    public DbSet<Statistics> Stats { get; set; }
}