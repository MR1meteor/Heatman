using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace ReportService.Clients.Interfaces;

public interface IBrigadeServiceClient : ITransient
{
    Task<Result<List<Guid>>> GetBrigadeEmployeeIdsAsync(Guid brigadeId);
}