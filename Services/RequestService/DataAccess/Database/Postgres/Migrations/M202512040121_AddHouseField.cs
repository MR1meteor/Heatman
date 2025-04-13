using FluentMigrator;

namespace RequestService.DataAccess.Database.Postgres.Migrations;

[Migration(1)]
public class M202512040121_AddHouseField : FluentMigrator.Migration
{
    public override void Up()
    {
        Alter.Table("requests").AddColumn("house").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Column("house").FromTable("requests");
    }
}