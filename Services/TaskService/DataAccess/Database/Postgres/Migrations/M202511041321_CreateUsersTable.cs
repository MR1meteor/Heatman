using FluentMigrator;

namespace TaskService.DataAccess.Database.Postgres.Migrations;

[Migration(0)]
public class M202511041321_CreateBrigadesAndBrigadeUsersTables : FluentMigrator.Migration
{
    public override void Up()
    {
        Create.Table("brigades")
            .WithColumn("id").AsGuid().NotNullable().Unique().PrimaryKey().WithDefault(SystemMethods.NewGuid)
            .WithColumn("creation_date").AsDateTime().NotNullable();

        Create.Table("brigade_employees")
            .WithColumn("brigade_id").AsGuid().NotNullable().ForeignKey("FK_brigade_employees_brigades", "brigades", "id")
            .WithColumn("employee_id").AsGuid().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("brigades");
        Delete.Table("brigade_employees");
    }
}