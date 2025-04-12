using Shared.DependencyInjection.Interfaces;

namespace FileService.Services.Interfaces;

public interface IMinioService : ITransient
{
    Task UploadFileAsync(Stream stream, string fileName, string contentType, long size);
    Task<string> GeneratePresignedUrlAsync(string fileName, int expirySeconds = 3600);
    Task<bool> FileExistsAsync(string fileName);
}