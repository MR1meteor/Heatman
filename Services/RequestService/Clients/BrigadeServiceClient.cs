using System.Net.Http.Headers;
using System.Text.Json;
using RequestService.Clients.Interfaces;
using Shared.ResultPattern.Models;

namespace RequestService.Clients;

public class BrigadeServiceClient : IBrigadeServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<BrigadeServiceClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BrigadeServiceClient(IConfiguration configuration, HttpClient httpClient, ILogger<BrigadeServiceClient> logger, IHttpContextAccessor httpContextAccessor)
    {
        _baseUrl = configuration.GetSection("Cluster")["BrigadeServiceUrl"];
        _httpClient = httpClient;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<Result<Guid>> GetPersonalBrigadeId()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(token))
        {
            // Console.WriteLine($"TESTT: {token}");
        }

        Console.WriteLine($"TESTT: {token}");
        var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/brigade");
        request.Headers.Authorization = AuthenticationHeaderValue.Parse(token);
        
        var response = await _httpClient.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"brigade-service: get personal brigade id returned {response.StatusCode}: {responseContent}");
            return Result<Guid>.Failure($"Microservice request failure: {responseContent}");
        }
        
        var brigadeId = JsonSerializer.Deserialize<Guid>(responseContent);
        return Result<Guid>.Success(brigadeId);
    }
}