using FluentMigrator;

namespace AuthService.DataAccess.Database.Postgres.Migrations;

[Migration(0)]
public class M202511041321_CreateUsersTable : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().NotNullable().Unique().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("verification_code").AsString().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("users");
    }
}