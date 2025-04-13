using FileService.Services.Interfaces;

namespace FileService.Services;

public class FileService : IFileService
{
    private readonly IMinioService _minioService;

    public FileService(IMinioService minioService)
    {
        _minioService = minioService;
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        using var stream = file.OpenReadStream();

        await _minioService.UploadFileAsync(stream, fileName, file.ContentType, file.Length);
        return fileName;
    }

    public async Task<string?> GetFileUrlAsync(string fileName)
    {
        var exists = await _minioService.FileExistsAsync(fileName);
        if (!exists) return null;

        return await _minioService.GeneratePresignedUrlAsync(fileName);
    }

    public async Task<string?> GetFileAsBase64Async(string fileName)
    {
        var bytes = await _minioService.GetFileAsync(fileName);
        return bytes != null ? Convert.ToBase64String(bytes) : null;
    }
}