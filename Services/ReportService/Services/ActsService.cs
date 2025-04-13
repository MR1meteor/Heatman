using ReportService.Clients.Interfaces;
using ReportService.DataAccess.Repositories.Interfaces;
using ReportService.Mapping;
using ReportService.Models.Db;
using ReportService.Models.Dtos;
using ReportService.Services.Interfaces;

namespace ReportService.Services;

public class ActsService : IActsService
{
    private readonly IRequestServiceClient _requestServiceClient;
    private readonly IBrigadeServiceClient _brigadeServiceClient;
    private readonly IAuthServiceClient _authServiceClient;
    private readonly IControlActRepository _controlActRepository;
    private readonly IStopResumeActRepository _stopResumeActRepository;
    private readonly IActFileProcessor _actFileProcessor;
    private readonly IFileServiceClient _fileServiceClient;

    public ActsService(IRequestServiceClient requestServiceClient,
        IBrigadeServiceClient brigadeServiceClient,
        IAuthServiceClient authServiceClient,
        IControlActRepository controlActRepository,
        IStopResumeActRepository stopResumeActRepository,
        IActFileProcessor actFileProcessor,
        IFileServiceClient fileServiceClient)
    {
        _requestServiceClient = requestServiceClient;
        _brigadeServiceClient = brigadeServiceClient;
        _authServiceClient = authServiceClient;
        _controlActRepository = controlActRepository;
        _stopResumeActRepository = stopResumeActRepository;
        _actFileProcessor = actFileProcessor;
        _fileServiceClient = fileServiceClient;
    }
    
    public async Task<bool> CreateControlActAsync(CreateControlActRequest createControlActRequest)
    {
        var requestResult = await _requestServiceClient.GetRequestByIdAsync(createControlActRequest.RequestId);

        if (requestResult.IsFailure || requestResult.Data == null)
        {
            return false;
        }
        
        var brigadeUserIdsResult = await _brigadeServiceClient.GetBrigadeEmployeeIdsAsync(requestResult.Data.BrigadeId);

        if (brigadeUserIdsResult.IsFailure || brigadeUserIdsResult.Data == null || brigadeUserIdsResult.Data.Count != 2)
        {
            return false;
        }

        var usersResult = await _authServiceClient.GetUsersByIdsAsync(brigadeUserIdsResult.Data);

        if (usersResult.IsFailure || usersResult.Data == null || usersResult.Data.Count != 2)
        {
            return false;
        }

        var request = requestResult.Data;

        var newDbAct = new DbControlAct
        {
            Id = Guid.NewGuid(),
            RequestId = createControlActRequest.RequestId,
            WorkTime = request?.WorkTime ?? DateTime.Now,
            Address = request?.Address ?? string.Empty,
            HasCommutingDevice = createControlActRequest.HasCommutingDevice,
            HasViolation = createControlActRequest.HasViolation,
            MeteringDeviceLocationType = (int)createControlActRequest.MeteringDeviceLocationType,
            MeteringDeviceLocation = createControlActRequest.MeteringDeviceLocation,
            DeviceReadings = createControlActRequest.DeviceReadings,
            Workers = usersResult.Data.Select(user => user.FullName).ToArray(),
        };
        
        await _controlActRepository.InsertAsync(newDbAct);
        return true;
    }

    public async Task<bool> CreateStopResumeActAsync(CreateStopResumeActRequest createStopResumeActRequest)
    {
        var requestResult = await _requestServiceClient.GetRequestByIdAsync(createStopResumeActRequest.RequestId);

        if (requestResult.IsFailure || requestResult.Data == null)
        {
            return false;
        }

        var brigadeUserIdsResult = await _brigadeServiceClient.GetBrigadeEmployeeIdsAsync(requestResult.Data.BrigadeId);

        if (brigadeUserIdsResult.IsFailure || brigadeUserIdsResult.Data == null || brigadeUserIdsResult.Data.Count != 2)
        {
            return false;
        }

        var usersResult = await _authServiceClient.GetUsersByIdsAsync(brigadeUserIdsResult.Data);

        if (usersResult.IsFailure || usersResult.Data == null || usersResult.Data.Count != 2)
        {
            return false;
        }
        
        var request = requestResult.Data;

        var newDbAct = new DbStopResumeAct
        {
            Id = Guid.NewGuid(),
            RequestId = createStopResumeActRequest.RequestId,
            Type = (int)request.Type, // Типы согласованы
            WorkTime = request?.WorkTime ?? DateTime.Now,
            Address = request?.Address ?? string.Empty,
            HasCommutingDevice = createStopResumeActRequest.HasCommutingDevice,
            Result = (int)createStopResumeActRequest.Result,
            MeteringDeviceLocationType = (int)createStopResumeActRequest.MeteringDeviceLocationType,
            MeteringDeviceLocation = createStopResumeActRequest.MeteringDeviceLocation,
            DeviceReadings = createStopResumeActRequest.DeviceReadings,
            WorkMethodType = (int)createStopResumeActRequest.WorkMethodType,
            Workers = usersResult.Data.Select(user => user.FullName).ToArray(),
            ClientFullName = createStopResumeActRequest.ClientFullName
        };
        
        await _stopResumeActRepository.InsertAsync(newDbAct);
        return true;
    }

    public async Task<string> GetControlActAsync(Guid requestId)
    {
        var controlAct = (await _controlActRepository.GetByRequestIdAsync(requestId)).MapToDomain();

        if (controlAct == null)
        {
            return string.Empty;
        }

        var base64Act = _actFileProcessor.FillTemplateBase64(controlAct, "./../Models/Templates/act_control_template.docx");
        var fileNameResult = await _fileServiceClient.UploadFileAsync(base64Act);

        if (fileNameResult.IsFailure || string.IsNullOrWhiteSpace(fileNameResult.Data))
        {
            return string.Empty;
        }

        var fileUrl = await _fileServiceClient.GetUrlAsync(fileNameResult.Data);
        return fileUrl.IsSuccess ? fileUrl.Data : string.Empty;
    }
}