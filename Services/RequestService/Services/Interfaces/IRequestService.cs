using RequestService.Models.Db;
using RequestService.Models.Domain;
using RequestService.Models.Dtos;
using Shared.DependencyInjection.Interfaces;

namespace RequestService.Services.Interfaces;

public interface IRequestService : ITransient
{
    Task<List<Request>> GetPersonalAsync();
    Task<List<Request>> GetByBrigadeAsync(Guid brigadeId);
    Task<bool> CreateAsync(CreateNewRequest request);
    Task<bool> CreateByExcelFileAsync(byte[] fileBytes);
    Task<bool> SetCompletedStatusAsync(Guid requestId);
    Task<bool> UploadBeforeFileAsync(Guid requestId, byte[] fileBytes);
    Task<bool> UploadAfterFileAsync(Guid requestId, byte[] fileBytes);
    Task<Request?> GetByIdAsync(Guid requestId);
}