using RequestService.DataAccess.Repositories.Interfaces;
using RequestService.Mapping;
using RequestService.Models.Domain;
using RequestService.Models.Dtos;
using RequestService.Services.Interfaces;

namespace RequestService.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;

    public RequestService(IRequestRepository requestRepository)
    {
        _requestRepository = requestRepository;
    }
    
    public async Task<List<Request>> GetByBrigadeAsync(Guid brigadeId) =>
        (await _requestRepository.GetByBrigadeAsync(brigadeId)).MapToDomain();

    public Task<bool> CreateAsync(CreateNewRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetCompletedStatusAsync(Guid requestId)
    {
        throw new NotImplementedException();
    }
}