using Shared.DependencyInjection.Interfaces;

namespace AuthService.Services.Interfaces;

public interface IAuthService : ITransient
{
    Task<string> LoginByVerificationCodesAsync(string firstCode, string secondCode);
}