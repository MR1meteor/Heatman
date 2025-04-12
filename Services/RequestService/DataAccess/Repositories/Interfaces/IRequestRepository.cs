using RequestService.Models.Db;
using Shared.DependencyInjection.Interfaces;

namespace RequestService.DataAccess.Repositories.Interfaces;

public interface IRequestRepository : ITransient
{
    Task<List<DbRequest>> GetByBrigadeAsync(Guid brigadeId);
    Task AddAsync(DbRequest request);
    Task AddAsync(IEnumerable<DbRequest> requests);
    Task SetBeforeImageAsync(Guid requestId, string imageName);
    Task SetAfterImageAsync(Guid requestId, string imageName);
}