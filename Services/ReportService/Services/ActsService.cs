using ReportService.Clients.Interfaces;
using ReportService.DataAccess.Repositories.Interfaces;
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

    public ActsService(IRequestServiceClient requestServiceClient,
        IBrigadeServiceClient brigadeServiceClient,
        IAuthServiceClient authServiceClient,
        IControlActRepository controlActRepository,
        IStopResumeActRepository stopResumeActRepository)
    {
        _requestServiceClient = requestServiceClient;
        _brigadeServiceClient = brigadeServiceClient;
        _authServiceClient = authServiceClient;
        _controlActRepository = controlActRepository;
        _stopResumeActRepository = stopResumeActRepository;
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
}