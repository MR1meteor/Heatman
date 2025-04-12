using System.Net.Http.Headers;
using RequestService.Clients.Interfaces;
using RequestService.Helpers;
using Shared.ResultPattern.Models;

namespace RequestService.Clients;

public class FileServiceClient : IFileServiceClient
{
    private readonly string _baseUrl;
    private readonly ILogger<FileServiceClient> _logger;
    private readonly HttpClient _httpClient;

    public FileServiceClient(IConfiguration configuration, HttpClient httpClient, ILogger<FileServiceClient> logger)
    {
        _baseUrl = configuration.GetSection("Cluster")["FileServiceUrl"];
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<Result<string>> UploadFileAsync(byte[] fileData)
    {
        var contentType = MimeHelper.DetectMimeType(fileData);
        var extension = MimeHelper.GetFileExtension(contentType);
        var fileName = $"{Guid.NewGuid()}{extension}";
        
        using var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(fileData);
        fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
        
        content.Add(fileContent, "file", fileName);

        var response = await _httpClient.PostAsync(_baseUrl, content);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError($"file-service: upload file returned {response.StatusCode}: {responseContent}");
            return Result<string>.Failure($"Microservice request failure: {responseContent}");
        }

        return Result<string>.Success(responseContent);
    }
}