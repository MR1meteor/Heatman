using EmployeeService.Models.Db;
using Shared.DependencyInjection.Interfaces;

namespace EmployeeService.DataAccess.Repositories.Interfaces;

public interface IEmployeeRepository : ITransient
{
    Task<DbEmployee?> GetById(Guid id);
}