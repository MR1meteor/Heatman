using ReportService.Models.Db;
using ReportService.Models.Domain;
using ReportService.Models.Enums;

namespace ReportService.Mapping;

public static class ControlActMapper
{
    public static ControlAct? MapToDomain(this DbControlAct? db)
    {
        return db == null
            ? null
            : new ControlAct
            {
                Id = db.Id,
                RequestId = db.RequestId,
                WorkTime = db.WorkTime,
                Address = db.Address,
                HasCommutingDevice = db.HasCommutingDevice,
                HasViolation = db.HasViolation,
                MeteringDeviceLocationType = (MeteringDeviceLocation)db.MeteringDeviceLocationType,
                MeteringDeviceLocation = db.MeteringDeviceLocation,
                DeviceReadings = db.DeviceReadings,
                Workers = db.Workers
            };
    }
}