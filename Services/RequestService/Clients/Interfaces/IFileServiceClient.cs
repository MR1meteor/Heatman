using Shared.DependencyInjection.Interfaces;
using Shared.ResultPattern.Models;

namespace RequestService.Clients.Interfaces;

public interface IFileServiceClient : ITransient
{
    Task<Result<string>> UploadFileAsync(byte[] fileData);
}