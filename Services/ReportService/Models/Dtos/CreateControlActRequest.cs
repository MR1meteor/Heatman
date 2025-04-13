using ReportService.Models.Enums;

namespace ReportService.Models.Dtos;

public record CreateControlActRequest
{
    public Guid RequestId { get; set; }
    public bool HasViolation { get; set; }
    public bool HasCommutingDevice { get; set; }
    public MeteringDeviceLocation MeteringDeviceLocationType { get; set; }
    public string MeteringDeviceLocation { get; set; } = string.Empty;
    public string DeviceReadings { get; set; } = string.Empty;
}