using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace RequestService.Clients.Interfaces;

public interface IBrigadeServiceClient : ITransient
{
    Task<Result<Guid>> GetPersonalBrigadeId();
}