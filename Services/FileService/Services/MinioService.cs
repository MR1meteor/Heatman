using FileService.Services.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace FileService.Services;

public class MinioService : IMinioService
{
    private readonly MinioClient _minioClient;
    private readonly string _bucket;

    public MinioService(IConfiguration config, MinioClient minioClient)
    {
        _minioClient = minioClient;
        _bucket = config["Minio:BucketName"]!;
    }

    public async Task UploadFileAsync(Stream stream, string fileName, string contentType, long size)
    {
        var exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucket));
        if (!exists)
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucket));

        var putArgs = new PutObjectArgs()
            .WithBucket(_bucket)
            .WithObject(fileName)
            .WithStreamData(stream)
            .WithObjectSize(size)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(putArgs);
    }

    public async Task<bool> FileExistsAsync(string fileName)
    {
        try
        {
            await _minioClient.StatObjectAsync(new StatObjectArgs().WithBucket(_bucket).WithObject(fileName));
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GeneratePresignedUrlAsync(string fileName, int expirySeconds = 3600)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_bucket)
            .WithObject(fileName)
            .WithExpiry(expirySeconds);

        return await _minioClient.PresignedGetObjectAsync(args);
    }
}