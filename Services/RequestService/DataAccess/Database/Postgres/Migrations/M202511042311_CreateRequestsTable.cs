using FluentMigrator;

namespace RequestService.DataAccess.Database.Postgres.Migrations;

[Migration(0)]
public class M202511042311_CreateRequestsTable : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("requests")
            .WithColumn("id").AsGuid().NotNullable().Unique().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("address").AsString()
            .WithColumn("device").AsString()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("type").AsInt32().NotNullable()
            .WithColumn("creation_time").AsDateTime()
            .WithColumn("work_time").AsDateTime()
            .WithColumn("completion_time").AsDateTime()
            .WithColumn("brigade_id").AsGuid();
    }

    public override void Down()
    {
        Delete.Table("requests");
    }
}