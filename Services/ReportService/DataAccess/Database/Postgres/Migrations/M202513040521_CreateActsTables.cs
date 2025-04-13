using FluentMigrator;

namespace ReportService.DataAccess.Database.Postgres.Migrations;

[Migration(0)]
public class M202513040521_CreateActsTables : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("control_acts")
            .WithColumn("id").AsGuid().NotNullable().Unique().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("request_id").AsGuid().NotNullable()
            .WithColumn("has_violation").AsBoolean().NotNullable()
            .WithColumn("work_time").AsDateTime()
            .WithColumn("address").AsString()
            .WithColumn("has_commuting_device").AsBoolean().NotNullable()
            .WithColumn("metering_device_location_type").AsInt32().NotNullable()
            .WithColumn("metering_device_location").AsString().Nullable()
            .WithColumn("device_readings").AsString().Nullable()
            .WithColumn("workers").AsCustom("text[]").NotNullable();

        Create.Table("stop_resume_acts")
            .WithColumn("id").AsGuid().NotNullable().Unique().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("request_id").AsGuid().NotNullable()
            .WithColumn("type").AsInt32().NotNullable()
            .WithColumn("work_time").AsDateTime()
            .WithColumn("address").AsString()
            .WithColumn("has_commuting_device").AsBoolean().NotNullable()
            .WithColumn("result").AsInt32().NotNullable()
            .WithColumn("work_method").AsString().Nullable()
            .WithColumn("metering_device_location_type").AsInt32().NotNullable()
            .WithColumn("metering_device_location").AsString()
            .WithColumn("device_readings").AsString()
            .WithColumn("work_method_type").AsInt32().NotNullable()
            .WithColumn("workers").AsCustom("text[]").NotNullable()
            .WithColumn("client_full_name").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Table("stop_resume_acts");
        Delete.Table("control_acts");
    }
}