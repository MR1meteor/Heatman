using RequestService.Models.Enums;

namespace RequestService.Models.Dtos;

public record GetRequest
{
    public Guid Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Flat { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public RequestType Type { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime WorkTime { get; set; }
    public DateTime CompletionTime { get; set; }
    public string GeoTag { get; set; } = string.Empty;
    public string BeforeImage { get; set; } = string.Empty;
    public string AfterImage { get; set; } = string.Empty;
}