using RequestService.Models.Enums;

namespace RequestService.Models.Domain;

public class Request
{
    public Guid Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public RequestStatus Status { get; set; }
    public RequestType Type { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime WorkTime { get; set; }
    public DateTime CompletionTime { get; set; }
    public Guid BrigadeId { get; set; }
}