using BrigadeService.DataAccess.Repositories.Interfaces;
using BrigadeService.Mapping;
using BrigadeService.Models.Domain;
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

        // TODO: Избавиться фулл
        // var isEmployeesBusy = await _brigadeEmployeeRepository.ExistsTodayByEmployeesAsync(new[] { firstUserId.Value, secondUserId.Value });
        //
        // if (isEmployeesBusy)
        // {
        //     return Result<Guid>.Failure("Employee(s) already in brigade(s)");
        // }

        var brigadeId = await _brigadeRepository.CreateTodayAsync();

        if (brigadeId == null)
        {
            return Result<Guid>.Failure("Database error");
        }

        var brigadeEmployees = new List<BrigadeEmployee>
        {
            new BrigadeEmployee
            {
                BrigadeId = brigadeId.Value,
                EmployeeId = firstUserId.Value
            },
            new BrigadeEmployee
            {
                BrigadeId = brigadeId.Value,
                EmployeeId = secondUserId.Value
            }
        };

        var insertBrigadeEmployeesTasks = brigadeEmployees.Select(employee => _brigadeEmployeeRepository.InsertAsync(employee.MapToDb()));

        await Task.WhenAll(insertBrigadeEmployeesTasks);
        
        return Result<Guid>.Success(brigadeId.Value);
    }
}