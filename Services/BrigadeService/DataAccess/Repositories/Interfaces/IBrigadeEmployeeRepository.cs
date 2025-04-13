using BrigadeService.Models.Db;
using Shared.DependencyInjection.Interfaces;

namespace BrigadeService.DataAccess.Repositories.Interfaces;

public interface IBrigadeEmployeeRepository : ITransient
{
    Task<bool> ExistsTodayByEmployeesAsync(IEnumerable<Guid>? employeeIds);
    Task InsertAsync(DbBrigadeEmployee? employees);
    Task<List<Guid>> GetIdsByBrigadeId(Guid brigadeId);
}