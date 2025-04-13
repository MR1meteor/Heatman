using BrigadeService.DataAccess.Repositories.Interfaces;
using BrigadeService.DataAccess.Repositories.Sql.BrigadeEmployee;
using BrigadeService.Models.Db;
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
    
    public async Task<bool> ExistsTodayByEmployeesAsync(IEnumerable<Guid>? employeeIds)
    {
        if (employeeIds == null || !employeeIds.Any())
        {
            return false;
        }
        
        var parameters = new
        {
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(1),
            EmployeeIds = employeeIds
        };

        return await _dapperContext.CommandWithResponse<bool>(new QueryObject(SqlScripts.ExistsTodayByEmployees, parameters));
    }

    public async Task InsertAsync(DbBrigadeEmployee? employee)
    {
        if (employee == null)
        {
            return;
        }
        
        var parameters = new
        {
            BrigadeId = employee.BrigadeId,
            EmployeeId = employee.EmployeeId
        };
        
        await _dapperContext.Command<DbBrigadeEmployee>(new QueryObject(SqlScripts.Insert, parameters));
    }

    public async Task<List<Guid>> GetIdsByBrigadeId(Guid brigadeId)
    {
        var parameters = new
        {
            BrigadeId = brigadeId
        };

        return await _dapperContext.ListOrEmpty<Guid>(new QueryObject(SqlScripts.GetIdsByBrigadeId, parameters));
    }
}