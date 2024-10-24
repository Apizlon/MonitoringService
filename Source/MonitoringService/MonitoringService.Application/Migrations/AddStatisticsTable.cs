using FluentMigrator;

namespace MonitoringService.Application.Migrations;

/// <summary>
/// Миграция для добавлния таблицы со статистикой
/// </summary>
[Migration(2024102400)]
public class AddStatisticsTable : Migration
{
    /// <summary>
    /// Вызывается при применении миграции
    /// </summary>
    public override void Up()
    {
        Create.Table("Statistics")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("DeviceName").AsString()
            .WithColumn("OperatingSystem").AsString()
            .WithColumn("Version").AsString()
            .WithColumn("LastUpdateDateTime").AsDateTime();
    }
    
    /// <summary>
    /// Вызывается при откате миграции
    /// </summary>
    public override void Down()
    {
        Delete.Table("Statistics");
    }
}