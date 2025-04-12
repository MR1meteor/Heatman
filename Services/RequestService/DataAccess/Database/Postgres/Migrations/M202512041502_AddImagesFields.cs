using FluentMigrator;

namespace RequestService.DataAccess.Database.Postgres.Migrations;

[Migration(2)]
public class M202512041502_AddImagesFields : FluentMigrator.Migration
{
    public override void Up()
    {
        Alter.Table("requests")
            .AddColumn("after_image").AsString().Nullable()
            .AddColumn("before_image").AsString().Nullable();
    }

    public override void Down()
    {
        Delete.Column("after_image").Column("before_image").FromTable("requests");
    }
}