using AuthService.Models.Db;
using Shared.DependencyInjection.Interfaces;

namespace AuthService.DataAccess.Repositories.Interfaces;

public interface IUserRepository : ITransient
{
    Task<DbUser?> GetByVerificationCode(string code);
}