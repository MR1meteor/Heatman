namespace AuthService.Models.Requests;

public record LoginCodesRequest
{
    public string FirstCode { get; set; } = string.Empty;
    public string SecondCode { get; set; } = string.Empty;
}