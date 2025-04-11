using Shared.DependencyInjection.Interfaces;

namespace BrigadeService.DataAccess.Repositories.Interfaces;

public interface IBrigadeEmployeeRepository : ITransient
{
    Task<bool> ExistsTodayByEmployeesAsync(IEnumerable<Guid> employeeIds);
}