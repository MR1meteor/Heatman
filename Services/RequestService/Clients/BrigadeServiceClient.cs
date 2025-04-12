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
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/brigade");
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