using ReportService.DataAccess.Repositories.Interfaces;
using ReportService.DataAccess.Repositories.Sql.ControlAct;
using ReportService.Models.Db;
using Shared.Dapper;
using Shared.Dapper.Interfaces;

namespace ReportService.DataAccess.Repositories;

public class ControlControlActRepository : IControlActRepository
{
    private readonly IDapperContext _dapperContext;

    public ControlControlActRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task InsertAsync(DbControlAct act)
    {
        var parameters = new
        {
            RequestId = act.RequestId,
            WorkTime = act.WorkTime,
            Address = act.Address,
            HasCommutingDevice = act.HasCommutingDevice,
            HasViolation = act.HasViolation,
            MeteringDeviceLocationType = act.MeteringDeviceLocationType,
            MeteringDeviceLocation = act.MeteringDeviceLocation,
            DeviceReadings = act.DeviceReadings,
            Workers = act.Workers
        };

        await _dapperContext.Command<DbControlAct>(new QueryObject(SqlScripts.Insert, parameters));
    }

    public async Task<DbControlAct?> GetByRequestIdAsync(Guid requestId)
    {
        var parameters = new
        {
            RequestId = requestId
        };

        return await _dapperContext.FirstOrDefault<DbControlAct>(new QueryObject(SqlScripts.GetByRequestId, parameters));
    }
}