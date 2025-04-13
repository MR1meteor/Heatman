using Shared.DependencyInjection.Interfaces;

namespace FileService.Services.Interfaces;

public interface IFileService : ITransient
{
    Task<string> UploadFileAsync(IFormFile file);
    Task<string?> GetFileUrlAsync(string fileName);
    Task<string?> GetFileAsBase64Async(string fileName);
}