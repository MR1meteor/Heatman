using AuthService.Models.Requests;
using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace AuthService.Clients.Interfaces;

public interface IBrigadeServiceClient : ITransient
{
    Task<Result<Guid>> CreateTodayAsync(CreateTodayBrigadeRequest? request);
    Task<Result<Guid>> GetTodayByUsersAsync(Guid firstUserId, Guid secondUserId);
}