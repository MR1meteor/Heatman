using AuthService.Models.Domain;
using Shared.DependencyInjection.Interfaces;

namespace AuthService.Services.Interfaces;

public interface IUserService : ITransient
{
    Task<List<User>> GetByIds(List<Guid> ids);
}