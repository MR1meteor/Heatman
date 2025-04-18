﻿using ReportService.Models.Enums;

namespace ReportService.Models.Dtos;

public record CreateStopResumeActRequest
{
    public Guid RequestId { get; set; }
    public bool HasCommutingDevice { get; set; }
    public StopResumeWorkResult Result { get; set; }
    public string WorkMethod { get; set; } = string.Empty;
    public MeteringDeviceLocation MeteringDeviceLocationType { get; set; }
    public string MeteringDeviceLocation { get; set; } = string.Empty;
    public string DeviceReadings { get; set; } = string.Empty;
    public StopResumeWorkMethod WorkMethodType { get; set; }
    public string ClientFullName { get; set; } = string.Empty;
}