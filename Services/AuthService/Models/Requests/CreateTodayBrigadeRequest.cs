namespace AuthService.Models.Requests;

public class CreateTodayBrigadeRequest
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
}