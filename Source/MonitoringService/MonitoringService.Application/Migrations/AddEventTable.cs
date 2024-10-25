using FluentMigrator;

namespace MonitoringService.Application.Migrations;

[Migration(2024102500)]
public class AddEventTable : Migration
{
    public override void Up()
    {
        Create.Table("Events")
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("StatisticsId").AsInt32()
            .WithColumn("EventDateTime").AsCustom("TIMESTAMPTZ")
            .WithColumn("Name").AsString()
            .WithColumn("Description").AsString();
        Create.ForeignKey("FK_Events_Statistics")
            .FromTable("Events").ForeignColumn("StatisticsId")
            .ToTable("Statistics").PrimaryColumn("Id")
            .OnDeleteOrUpdate(System.Data.Rule.Cascade);
    }

    public override void Down()
    {
        Delete.Table("Events");
    }
}