using FluentMigrator.Runner.Initialization;

namespace EmployeeService.DataAccess.Database.Postgres;

public class SettingsConnectionStringReader : IConnectionStringReader
{
    private readonly IConfiguration _configuration;
    
    public SettingsConnectionStringReader(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString(string connectionStringOrName)
    {
        return _configuration["Connections:Postgres"] ?? string.Empty;
    }

    public int Priority => int.MaxValue;
}