using Shared.DependencyInjection.Interfaces;

namespace BrigadeService.Services.Interfaces;

public interface IBrigadeEmployeesService : ITransient
{
    Task<List<Guid>> GetEmployeesIds(Guid brigadeId);
}