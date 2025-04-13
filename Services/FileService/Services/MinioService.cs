using FileService.Services.Interfaces;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace FileService.Services;

public class MinioService : IMinioService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucket;

    public MinioService(IConfiguration config, IMinioClient minioClient)
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

    public async Task<byte[]?> GetFileAsync(string objectName)
    {
        try
        {
            using var ms = new MemoryStream();

            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucket)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(ms)));

            return ms.ToArray();
        }
        catch (ObjectNotFoundException)
        {
            return null;
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