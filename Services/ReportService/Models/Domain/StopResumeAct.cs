using ReportService.Models.Enums;

namespace ReportService.Models.Domain;

public class StopResumeAct
{
    public Guid Id { get; set; }
    public StopResumeWorkType Type { get; set; }
    public DateTime WorkTime { get; set; }
    public string Address { get; set; } = string.Empty;
    public bool HasCommutingDevice { get; set; }
    public StopResumeWorkResult Result { get; set; }
    public string WorkMethod { get; set; } = string.Empty;
    public MeteringDeviceLocation MeteringDeviceLocationType { get; set; }
    public string MeteringDeviceLocation { get; set; } = string.Empty;
    public string DeviceReadings { get; set; } = string.Empty;
    public StopResumeWorkMethod WorkMethodType { get; set; }
    public string[] Workers { get; set; } = [];
    public string ClientFullName { get; set; } = string.Empty;
}