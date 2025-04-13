using ReportService.DataAccess.Repositories.Interfaces;
using ReportService.DataAccess.Repositories.Sql.StopResumeAct;
using ReportService.Models.Db;
using Shared.Dapper;
using Shared.Dapper.Interfaces;

namespace ReportService.DataAccess.Repositories;

public class StopResumeActRepository : IStopResumeActRepository
{
    private readonly IDapperContext _dapperContext;

    public StopResumeActRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task InsertAsync(DbStopResumeAct act)
    {
        var parameters = new
        {
            RequestId = act.RequestId,
            Type = act.Type,
            WorkTime = act.WorkTime,
            Address = act.Address,
            HasCommutingDevice = act.HasCommutingDevice,
            Result = act.Result,
            WorkMethod = act.WorkMethod,
            MeteringDeviceLocationType = act.MeteringDeviceLocationType,
            MeteringDeviceLocation = act.MeteringDeviceLocation,
            DeviceReadings = act.DeviceReadings,
            WorkMethodType = act.WorkMethodType,
            Workers = act.Workers,
            ClientFullName = act.ClientFullName
        };

        await _dapperContext.Command<DbStopResumeAct>(new QueryObject(SqlScripts.Insert, parameters));
    }

    public async Task<DbStopResumeAct?> GetByRequestIdAsync(Guid requestId)
    {
        var parameters = new
        {
            RequestId = requestId
        };

        return await _dapperContext.FirstOrDefault<DbStopResumeAct>(new QueryObject(SqlScripts.GetByRequestId, parameters));
    }
}