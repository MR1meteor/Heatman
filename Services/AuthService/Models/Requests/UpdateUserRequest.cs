namespace AuthService.Models.Requests;

public record UpdateUserRequest
{
    public string VerificationCode { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}