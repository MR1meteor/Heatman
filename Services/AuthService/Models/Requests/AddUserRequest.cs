namespace AuthService.Models.Requests;

public record AddUserRequest
{
    public string VerificationCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
}