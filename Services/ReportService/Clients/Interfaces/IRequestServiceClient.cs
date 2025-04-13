using ReportService.Models.Domain;
using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace ReportService.Clients.Interfaces;

public interface IRequestServiceClient : ITransient
{
    Task<Result<Request>> GetRequestByIdAsync(Guid requestId);
}