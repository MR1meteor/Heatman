using System.Text.Json;
using ReportService.Clients.Interfaces;
using ReportService.Models.Domain;
using Shared.ResultPattern.Models;

namespace ReportService.Clients;

public class RequestServiceClient : IRequestServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<RequestServiceClient> _logger;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestServiceClient(IConfiguration configuration, ILogger<RequestServiceClient> logger, HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _baseUrl = configuration.GetSection("Cluster")["RequestServiceUrl"];
        _logger = logger;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<Result<Request>> GetRequestByIdAsync(Guid requestId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{requestId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"request-service: get request by id returned {response.StatusCode}: {responseContent}");
            return Result<Request>.Failure("Microservice error");
        }
        
        var request = JsonSerializer.Deserialize<Request>(responseContent);
        return Result<Request>.Success(request);
    }
}