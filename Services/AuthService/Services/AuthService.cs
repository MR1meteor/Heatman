using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Clients.Interfaces;
using AuthService.DataAccess.Repositories.Interfaces;
using AuthService.Mapping;
using AuthService.Models.Domain;
using AuthService.Models.Requests;
using AuthService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IBrigadeServiceClient _brigadeServiceClient;

    public AuthService(IConfiguration configuration, IUserRepository userRepository, IBrigadeServiceClient brigadeServiceClient)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _brigadeServiceClient = brigadeServiceClient;
    }
    
    public async Task<string> LoginByVerificationCodesAsync(string firstCode, string secondCode)
    {
        if (string.IsNullOrWhiteSpace(firstCode) || string.IsNullOrWhiteSpace(secondCode) || firstCode == secondCode)
        {
            return string.Empty;
        }
        
        var firstUserTask = _userRepository.GetByVerificationCode(firstCode);
        var secondUserTask = _userRepository.GetByVerificationCode(secondCode);

        await Task.WhenAll(firstUserTask, secondUserTask);

        var firstUser = firstUserTask.Result;
        var secondUser = secondUserTask.Result;

        if (firstUser == null || secondUser == null)
        {
            return string.Empty;
        }
        
        // TODO: Взаимодействие с МКС бригад: создание бригады на текущий день, проверка на присутствие кого-то уже в бригаде
        var createBrigadeResponse = await _brigadeServiceClient.CreateTodayAsync(new CreateTodayBrigadeRequest {FirstUserId = firstUser.Id, SecondUserId = secondUser.Id});

        return !createBrigadeResponse.IsSuccess ? string.Empty : GenerateJwtToken(firstUser.MapToDomain(), secondUser.MapToDomain());
    }

    private string GenerateJwtToken(User? firstUser, User? secondUser)
    {
        if (firstUser == null || secondUser == null)
        {
            return string.Empty;
        }
        
        var claims = new[]
        {
            new Claim("User1Id", firstUser.Id.ToString()),
            new Claim("user2Id", secondUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "Employee")
        };

        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(double.Parse(jwtSettings["ExpiresInHours"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}