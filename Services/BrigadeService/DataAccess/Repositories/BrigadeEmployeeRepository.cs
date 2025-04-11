using BrigadeService.DataAccess.Repositories.Interfaces;
using BrigadeService.DataAccess.Repositories.Sql.BrigadeEmployee;
using Shared.Dapper;
using Shared.Dapper.Interfaces;

namespace BrigadeService.DataAccess.Repositories;

public class BrigadeEmployeeRepository : IBrigadeEmployeeRepository
{
    private readonly IDapperContext _dapperContext;

    public BrigadeEmployeeRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<bool> ExistsTodayByEmployeesAsync(IEnumerable<Guid> employeeIds)
    {
        var parameters = new
        {
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(1),
            EmployeeIds = employeeIds
        };

        return await _dapperContext.CommandWithResponse<bool>(new QueryObject(SqlScripts.ExistsTodayByEmployees, parameters));
    }
}