﻿using System.Text.Json;
using ReportService.Clients.Interfaces;
using ReportService.Models.Domain;
using Shared.ResultPattern.Models;

namespace ReportService.Clients;

public class RequestServiceClient : IRequestServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<RequestServiceClient> _logger;
    private readonly HttpClient _httpClient;

    public RequestServiceClient(IConfiguration configuration, ILogger<RequestServiceClient> logger, HttpClient httpClient)
    {
        _baseUrl = configuration.GetSection("Cluster")["RequestServiceUrl"];
        _logger = logger;
        _httpClient = httpClient;
    }
    
    public async Task<Result<Request>> GetRequestByIdAsync(Guid requestId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/api/requests/by-id/{requestId}");
        var responseContent = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"request-service: get request by id returned {response.StatusCode}: {responseContent}");
            return Result<Request>.Failure("Microservice error");
        }

        var request = JsonSerializer.Deserialize<Request>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return Result<Request>.Success(request);
    }
}