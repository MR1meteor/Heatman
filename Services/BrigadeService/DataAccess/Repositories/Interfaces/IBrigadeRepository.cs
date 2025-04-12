using Shared.DependencyInjection.Interfaces;

namespace BrigadeService.DataAccess.Repositories.Interfaces;

public interface IBrigadeRepository : ITransient
{
    Task<Guid?> CreateTodayAsync();
    Task<Guid?> GetTodayByEmployeeIdsAsync(IEnumerable<Guid> employeeIds);
}