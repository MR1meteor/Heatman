using EmployeeService.DataAccess.Database.Dapper;
using EmployeeService.DataAccess.Database.Dapper.Interfaces;
using EmployeeService.DataAccess.Repositories.Interfaces;
using EmployeeService.DataAccess.Repositories.Sql.Employee;
using EmployeeService.Models.Db;

namespace EmployeeService.DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly IDapperContext _dapperContext;

    public EmployeeRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public Task<DbEmployee?> GetById(Guid id) =>
        _dapperContext.FirstOrDefault<DbEmployee>(new QueryObject(SqlScripts.GetById, new { Id = id }));
}