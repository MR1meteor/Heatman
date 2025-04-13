using AuthService.DataAccess.Repositories.Interfaces;
using AuthService.Mapping;
using AuthService.Models.Domain;
using AuthService.Services.Interfaces;

namespace AuthService.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<List<User>> GetByIds(List<Guid> ids)
    {
        return (await _userRepository.GetByIds(ids)).MapToDomain();
    }
}