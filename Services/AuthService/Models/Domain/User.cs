namespace AuthService.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    public string VerificationCode { get; set; } = string.Empty;
}