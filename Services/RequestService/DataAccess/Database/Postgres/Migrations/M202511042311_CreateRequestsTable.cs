using FluentMigrator;

namespace RequestService.DataAccess.Database.Postgres.Migrations;

[Migration(0)]
public class M202511042311_CreateRequestsTable : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("requests")
            .WithColumn("id").AsGuid().NotNullable().Unique().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("city").AsString()
            .WithColumn("street").AsString()
            .WithColumn("room").AsString()
            .WithColumn("flat").AsString()
            .WithColumn("device").AsString()
            .WithColumn("status").AsInt32().NotNullable()
            .WithColumn("type").AsInt32().NotNullable()
            .WithColumn("creation_time").AsDateTime()
            .WithColumn("work_time").AsDateTime().Nullable()
            .WithColumn("completion_time").AsDateTime().Nullable()
            .WithColumn("brigade_id").AsGuid()
            .WithColumn("geotag").AsString();
    }

    public override void Down()
    {
        Delete.Table("requests");
    }
}