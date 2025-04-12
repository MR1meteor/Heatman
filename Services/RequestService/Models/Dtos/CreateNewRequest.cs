using RequestService.Models.Enums;

namespace RequestService.Models.Dtos;

public record CreateNewRequest
{
    public string Address { get; set; }
    public string Device { get; set; }
    public RequestType Type { get; set; }
}