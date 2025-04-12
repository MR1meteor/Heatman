using RequestService.Clients.Interfaces;
using RequestService.DataAccess.Repositories.Interfaces;
using RequestService.Mapping;
using RequestService.Models.Db;
using RequestService.Models.Domain;
using RequestService.Models.Dtos;
using RequestService.Models.Enums;
using RequestService.Services.Interfaces;

namespace RequestService.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _requestRepository;
    private readonly IBrigadeServiceClient _brigadeServiceClient;
    private readonly IExcelRequestParser _excelRequestParser;
    private readonly IFileServiceClient _fileServiceClient;
    
    public RequestService(IRequestRepository requestRepository,
        IBrigadeServiceClient brigadeServiceClient,
        IExcelRequestParser excelRequestParser,
        IFileServiceClient fileServiceClient)
    {
        _requestRepository = requestRepository;
        _brigadeServiceClient = brigadeServiceClient;
        _excelRequestParser = excelRequestParser;
        _fileServiceClient = fileServiceClient;
    }

    public async Task<List<Request>> GetPersonalAsync()
    {
        var brigadeIdResult = await _brigadeServiceClient.GetPersonalBrigadeId();

        return brigadeIdResult.IsFailure ? [] : (await _requestRepository.GetByBrigadeAsync(brigadeIdResult.Data)).MapToDomain();
    }

    public async Task<List<Request>> GetByBrigadeAsync(Guid brigadeId) =>
        (await _requestRepository.GetByBrigadeAsync(brigadeId)).MapToDomain();

    public async Task<bool> CreateAsync(CreateNewRequest request)
    {
        var brigadeId = await _brigadeServiceClient.GetPersonalBrigadeId();

        if (brigadeId.IsFailure)
        {
            return false;
        }

        var dbRequest = new DbRequest
        {
            Id = Guid.NewGuid(),
            City = request.City,
            Street = request.Street,
            House = request.House,
            Room = request.Room,
            Flat = request.Flat,
            Device = request.Device,
            Status = (int)RequestStatus.InWork,
            Type = (int)request.Type,
            CreationTime = DateTime.UtcNow,
            WorkTime = null,
            CompletionTime = null,
            BrigadeId = brigadeId.Data,
            GeoTag = string.Empty
        };

        await _requestRepository.AddAsync(dbRequest);
        return true;
    }

    public async Task<bool> CreateByExcelFileAsync(byte[] fileBytes)
    {
        var brigadeId = await _brigadeServiceClient.GetPersonalBrigadeId();

        if (brigadeId.IsFailure)
        {
            return false;
        }
        
        var excelRequests = _excelRequestParser.GetExcelRequestsAsync(fileBytes);

        if (excelRequests.Count == 0)
        {
            return false;
        }

        var dbRequests = excelRequests.Select(request => new DbRequest
        {
            Id = Guid.NewGuid(),
            City = request.City,
            Street = request.Street,
            House = request.House,
            Room = request.Room,
            Flat = request.Flat,
            Device = request.Device,
            Status = (int)RequestStatus.InWork,
            CreationTime = DateTime.UtcNow,
            WorkTime = null,
            CompletionTime = null,
            BrigadeId = brigadeId.Data,
            GeoTag = string.Empty
        });

        await _requestRepository.AddAsync(dbRequests);
        return true;
    }

    public Task<bool> SetCompletedStatusAsync(Guid requestId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UploadBeforeFileAsync(Guid requestId, byte[] fileBytes)
    {
        var uploadFileResult = await _fileServiceClient.UploadFileAsync(fileBytes);

        if (uploadFileResult.IsFailure || uploadFileResult.Data == null)
        {
            return false;
        }

        await _requestRepository.SetBeforeImageAsync(requestId, uploadFileResult.Data);
        return true;
    }

    public async Task<bool> UploadAfterFileAsync(Guid requestId, byte[] fileBytes)
    {
        var uploadFileResult = await _fileServiceClient.UploadFileAsync(fileBytes);

        if (uploadFileResult.IsFailure || uploadFileResult.Data == null)
        {
            return false;
        }

        await _requestRepository.SetAfterImageAsync(requestId, uploadFileResult.Data);
        return true;
    }
}