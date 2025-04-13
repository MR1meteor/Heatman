using AuthService.DataAccess.Repositories.Interfaces;
using AuthService.DataAccess.Repositories.Sql.User;
using AuthService.Models.Db;
using Shared.Dapper;
using Shared.Dapper.Interfaces;

namespace AuthService.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDapperContext _dapperContext;

    public UserRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public Task<DbUser?> GetByVerificationCode(string code) =>
        _dapperContext.FirstOrDefault<DbUser>(new QueryObject(SqlScripts.GetByVerificationCode, new { VerificationCode = code }));

    public Task<List<DbUser>> GetByIds(IEnumerable<Guid> ids) =>
        _dapperContext.ListOrEmpty<DbUser>(new QueryObject(SqlScripts.GetByIds, new { Ids = ids }));
}