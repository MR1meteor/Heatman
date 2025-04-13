using ReportService.Models.Enums;

namespace ReportService.Models.Domain;

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
    public string GeoTag { get; set; }
    public string BeforeImage { get; set; }
    public string AfterImage { get; set; }

    public string Address => string.Join(", ", new[]
    {
        City,
        Street,
        House,
        Flat,
        Room
    }.Where(s => !string.IsNullOrWhiteSpace(s)));
}