namespace RequestService.Models.Db;

public class DbRequest
{
    public Guid Id { get; set; }
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string House { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string Flat { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public int Status { get; set; }
    public int Type { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? WorkTime { get; set; }
    public DateTime? CompletionTime { get; set; }
    public Guid BrigadeId { get; set; }
    public string GeoTag { get; set; } = string.Empty;
    public string BeforeImage { get; set; } = string.Empty;
    public string AfterImage { get; set; } = string.Empty;
}