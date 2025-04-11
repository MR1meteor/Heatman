using System.Text;
using System.Text.Json;
using AuthService.Clients.Interfaces;
using Shared.ResultPattern.Models;

namespace AuthService.Clients;

public class BrigadeServiceClient : IBrigadeServiceClient
{
    private readonly string _baseUrl;
    private readonly HttpClient _httpClient;

    public BrigadeServiceClient(IConfiguration configuration, HttpClient httpClient)
    {
        _baseUrl = configuration.GetSection("Cluster")["BrigadeServiceUrl"];
        _httpClient = httpClient;
    }
    
    public async Task<Result<Guid>> CreateTodayAsync()
    {
        var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"{_baseUrl}/api/brigade", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return Result<Guid>.Failure($"Microservice request failure: {responseContent}");
        }

        var createdBrigadeGuid = JsonSerializer.Deserialize<Guid>(responseContent);
        return Result<Guid>.Success(createdBrigadeGuid);
    }
}