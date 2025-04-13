using System.Text.Json;
using ReportService.Clients.Interfaces;
using Shared.ResultPattern.Models;

namespace ReportService.Clients;

public class BrigadeServiceClient : IBrigadeServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<RequestServiceClient> _logger;
    private readonly HttpClient _httpClient;

    public BrigadeServiceClient(IConfiguration configuration, ILogger<RequestServiceClient> logger, HttpClient httpClient)
    {
        _baseUrl = configuration.GetSection("Cluster")["BrigadeServiceUrl"];
        _logger = logger;
        _httpClient = httpClient;
    }


    public async Task<Result<List<Guid>>> GetBrigadeEmployeeIdsAsync(Guid brigadeId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api//brigade/employees/by-brigade/{brigadeId}/ids");
        var responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            return Result<List<Guid>>.Failure($"Microservice error: {responseContent}");
        }
        
        var employeeIds = JsonSerializer.Deserialize<List<Guid>>(responseContent);
        return Result<List<Guid>>.Success(employeeIds);
    }
}