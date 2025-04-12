using RequestService.Models.Domain;
using RequestService.Models.Dtos;
using Shared.DependencyInjection.Interfaces;

namespace RequestService.Services.Interfaces;

public interface IRequestService : ITransient
{
    Task<List<Request>> GetByBrigadeAsync(Guid brigadeId);
    Task<bool> CreateAsync(CreateNewRequest request);
    Task<bool> SetCompletedStatusAsync(Guid requestId);
}