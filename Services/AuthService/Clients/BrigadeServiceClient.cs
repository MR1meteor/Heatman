using System.Text;
using System.Text.Json;
using AuthService.Clients.Interfaces;
using AuthService.Models.Requests;
using Shared.ResultPattern.Models;

namespace AuthService.Clients;

public class BrigadeServiceClient : IBrigadeServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<BrigadeServiceClient> _logger;
    private readonly HttpClient _httpClient;

    public BrigadeServiceClient(IConfiguration configuration, HttpClient httpClient, ILogger<BrigadeServiceClient> logger)
    {
        _baseUrl = configuration.GetSection("Cluster")["BrigadeServiceUrl"];
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<Result<Guid>> CreateTodayAsync(CreateTodayBrigadeRequest? request)
    {
        if (request == null)
        {
            return Result<Guid>.Failure("Invalid validation");
        }

        var json = JsonSerializer.Serialize(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/api/brigade", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"brigade-service: create today returned {response.StatusCode}: {responseContent}");
            return Result<Guid>.Failure($"Microservice request failure: {responseContent}");
        }

        var createdBrigadeGuid = JsonSerializer.Deserialize<Guid>(responseContent);
        return Result<Guid>.Success(createdBrigadeGuid);
    }
}