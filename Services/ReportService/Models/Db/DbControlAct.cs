﻿namespace ReportService.Models.Db;

public class DbControlAct
{
    public int Id { get; set; }
    public bool IsUnauthorizedConnection { get; set; }
    public DateTime WorkTime { get; set; }
    public string Address { get; set; } = string.Empty;
    public bool HasCommutingDevice { get; set; }
    public bool HasViolation { get; set; }
    public int MeteringDeviceLocationType { get; set; }
    public string MeteringDeviceLocation { get; set; } = string.Empty;
    public string DeviceReadings { get; set; } = string.Empty;
    public string[] Workers { get; set; } = [];
}