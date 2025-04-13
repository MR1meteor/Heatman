using Shared.ResultPattern.Models;

namespace ReportService.Clients.Interfaces;

public interface IFileServiceClient
{
    Task<Result<string>> UploadFileAsync(byte[] fileData);
    Task<Result<string>> GetUrlAsync(string fileName);
}