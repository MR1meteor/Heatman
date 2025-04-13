using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace ReportService.Clients.Interfaces;

public interface IFileServiceClient : ITransient
{
    Task<Result<string>> UploadFileAsync(byte[] fileData);
    Task<Result<string>> GetUrlAsync(string fileName);
}