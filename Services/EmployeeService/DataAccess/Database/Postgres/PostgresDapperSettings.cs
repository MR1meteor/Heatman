using EmployeeService.DataAccess.Database.Dapper.Interfaces;

namespace EmployeeService.DataAccess.Database.Postgres;

public class PostgresDapperSettings : IDapperSettings
{
    private readonly IConfiguration _configuration;

    public PostgresDapperSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string ConnectionString => _configuration["Connections:Postgres"] ?? string.Empty;
}