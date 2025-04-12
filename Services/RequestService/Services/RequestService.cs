using RequestService.Clients.Interfaces;
using RequestService.DataAccess.Repositories.Interfaces;
using RequestService.Mapping;
using RequestService.Models.Domain;
using RequestService.Models.Dtos;
using RequestService.Services.Interfaces;

namespace RequestService.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IBrigadeServiceClient _brigadeServiceClient;
    
    public RequestService(IRequestRepository requestRepository, IBrigadeServiceClient brigadeServiceClient)
    {
        _requestRepository = requestRepository;
        _brigadeServiceClient = brigadeServiceClient;
    }

    public async Task<List<Request>> GetPersonalAsync()
    {
        var brigadeIdResult = await _brigadeServiceClient.GetPersonalBrigadeId();

        return brigadeIdResult.IsFailure ? [] : (await _requestRepository.GetByBrigadeAsync(brigadeIdResult.Data)).MapToDomain();
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