using RequestService.Models.Enums;

namespace RequestService.Models.Domain;

public class Request
{
    public Guid Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string House { get; set; } = string.Empty;
    public string Flat { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public RequestType Type { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? WorkTime { get; set; }
    public DateTime? CompletionTime { get; set; }
    public Guid BrigadeId { get; set; }
    public string GeoTag { get; set; } = string.Empty;
}