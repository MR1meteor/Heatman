using System.Text.Json;
using ReportService.Clients.Interfaces;
using ReportService.Models.Domain;
using Shared.ResultPattern.Models;

namespace ReportService.Clients;

public class AuthServiceClient : IAuthServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<RequestServiceClient> _logger;
    private readonly HttpClient _httpClient;

    public AuthServiceClient(IConfiguration configuration, ILogger<RequestServiceClient> logger, HttpClient httpClient)
    {
        _baseUrl = configuration.GetSection("Cluster")["AuthServiceUrl"];
        _logger = logger;
        _httpClient = httpClient;
    }
    
    public async Task<Result<List<User>>> GetUsersByIdsAsync(IEnumerable<Guid> ids)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/auth/user/by-ids?ids={string.Join(',', ids)}");
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"auth-service: get users by ids returned {response.StatusCode}: {responseContent}");
            return Result<List<User>>.Failure($"Microservice error: {responseContent}");
        }
        
        var users = JsonSerializer.Deserialize<List<User>>(responseContent);
        return Result<List<User>>.Success(users);
    }
}