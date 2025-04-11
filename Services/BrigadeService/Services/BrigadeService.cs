using BrigadeService.DataAccess.Repositories.Interfaces;
using BrigadeService.Services.Interfaces;
using Shared.ResultPattern.Models;

namespace BrigadeService.Services;

public class BrigadeService : IBrigadeService
{
    private readonly IBrigadeRepository _brigadeRepository;
    private readonly IBrigadeEmployeeRepository _brigadeEmployeeRepository;
    
    public BrigadeService(IBrigadeRepository brigadeRepository, IBrigadeEmployeeRepository brigadeEmployeeRepository)
    {
        _brigadeRepository = brigadeRepository;
        _brigadeEmployeeRepository = brigadeEmployeeRepository;
    }
    
    public async Task<Result<Guid>> CreateTodayAsync(Guid? firstUserId, Guid? secondUserId)
    {
        if (firstUserId == null || secondUserId == null)
        {
            return Result<Guid>.Failure("Validation error");
        }

        var isEmployeesBusy = await _brigadeEmployeeRepository.ExistsTodayByEmployeesAsync(new[] { firstUserId.Value, secondUserId.Value });

        if (isEmployeesBusy)
        {
            return Result<Guid>.Failure("Employee(s) already in brigade(s)");
        }

        var brigadeId = await _brigadeRepository.CreateTodayAsync();

        return brigadeId == null ? Result<Guid>.Failure("Database error") : Result<Guid>.Success(brigadeId.Value);
    }
}