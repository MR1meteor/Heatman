namespace ReportService.Models.Db;

public class DbStopResumeAct
{
    public Guid Id { get; set; }
    public Guid RequestId { get; set; }
    public int Type { get; set; }
    public DateTime WorkTime { get; set; }
    public string Address { get; set; } = string.Empty;
    public bool HasCommutingDevice { get; set; }
    public int Result { get; set; }
    public string WorkMethod { get; set; } = string.Empty;
    public int MeteringDeviceLocationType { get; set; }
    public string MeteringDeviceLocation { get; set; } = string.Empty;
    public string DeviceReadings { get; set; } = string.Empty;
    public int WorkMethodType { get; set; }
    public string[] Workers { get; set; } = [];
    public string ClientFullName { get; set; } = string.Empty;
}