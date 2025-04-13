using ReportService.Models.Domain;
using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace ReportService.Clients.Interfaces;

public interface IAuthServiceClient : ITransient
{
    Task<Result<List<User>>> GetUsersByIdsAsync(IEnumerable<Guid> ids);
}