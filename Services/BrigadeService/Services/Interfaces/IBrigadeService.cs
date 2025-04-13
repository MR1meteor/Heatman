using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace BrigadeService.Services.Interfaces;

public interface IBrigadeService : ITransient
{
    Task<Result<Guid>> CreateTodayAsync(Guid? firstUserId, Guid? secondUserId);
    Task<Result<Guid>> GetIdByUserIds(string firstUserId, string secondUserId);
}