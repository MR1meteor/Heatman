using RequestService.DataAccess.Repositories.Interfaces;
using RequestService.DataAccess.Repositories.Sql.Request;
using RequestService.Models.Db;
using Shared.Dapper;
using Shared.Dapper.Interfaces;

namespace RequestService.DataAccess.Repositories;

public class RequestRepository : IRequestRepository
{
    private readonly IDapperContext _dapperContext;

    public RequestRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<List<DbRequest>> GetByBrigadeAsync(Guid brigadeId)
    {
        var parameters = new
        {
            BrigadeId = brigadeId
        };

        return await _dapperContext.ListOrEmpty<DbRequest>(new QueryObject(SqlScripts.GetByBrigade, parameters)) ?? [];
    }

    public async Task AddAsync(DbRequest request)
    {
        var parameters = new
        {
            City = request.City,
            Street = request.Street,
            House = request.House,
            Room = request.Room,
            Flat = request.Flat,
            Device = request.Device,
            Status = request.Status,
            Type = request.Type,
            CreationTime = request.CreationTime,
            WorkTime = request.WorkTime,
            CompletionTime = request.CompletionTime,
            BrigadeId = request.BrigadeId,
            GeoTag = request.GeoTag
        };

        await _dapperContext.Command<DbRequest>(new QueryObject(SqlScripts.Insert, parameters));
    }

    public async Task AddAsync(IEnumerable<DbRequest> requests)
    {
        var insertTasks = requests.Select(AddAsync);
        
        await Task.WhenAll(insertTasks);
    }
}