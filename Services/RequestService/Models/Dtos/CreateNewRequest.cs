using RequestService.Models.Enums;

namespace RequestService.Models.Dtos;

public record CreateNewRequest
{
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string House { get; set; } = string.Empty;
    public string Flat { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public RequestType Type { get; set; }
}