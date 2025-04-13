namespace BrigadeService.Models.Requests;

public record CreateBrigadeRequest
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
}