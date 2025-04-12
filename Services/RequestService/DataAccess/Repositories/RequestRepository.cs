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
}