using FluentMigrator;

namespace AuthService.DataAccess.Database.Postgres.Migrations;

[Migration(1)]
public class M202515040819_AddColumns : FluentMigrator.Migration
{
    public override void Up()
    {
        Alter.Table("users")
            .AddColumn("full_name").AsString().Nullable()
            .AddColumn("is_admin").AsBoolean().WithDefaultValue(false);
    }

    public override void Down()
    {
        Delete.Column("is_admin").Column("full_name").FromTable("users");
    }
}