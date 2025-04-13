namespace AuthService.Models.Domain;

public class User
{
    public Guid Id { get; set; }
    public string VerificationCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}