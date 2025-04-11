namespace AuthService.Models.Db;

public class DbUser
{
    public Guid Id { get; set; }
    public string VerificationCode { get; set; } = string.Empty;
}