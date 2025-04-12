namespace RequestService.Models.Db;

public class DbRequest
{
    public Guid Id { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Device { get; set; } = string.Empty;
    public int Status { get; set; }
    public int Type { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime WorkTime { get; set; }
    public DateTime CompletionTime { get; set; }
    public Guid BrigadeId { get; set; }
}