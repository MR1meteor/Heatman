using BrigadeService.DataAccess.Repositories.Interfaces;
using BrigadeService.Services.Interfaces;

namespace BrigadeService.Services;

public class BrigadeEmployeesService : IBrigadeEmployeesService
{
    private readonly IBrigadeEmployeeRepository _brigadeEmployeeRepository;

    public BrigadeEmployeesService(IBrigadeEmployeeRepository brigadeEmployeeRepository)
    {
        _brigadeEmployeeRepository = brigadeEmployeeRepository;
    }

    public Task<List<Guid>> GetEmployeesIds(Guid brigadeId) => _brigadeEmployeeRepository.GetIdsByBrigadeId(brigadeId);
}