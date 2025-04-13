using AuthService.Models.Requests;
using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCodesRequest request)
    {
        var token = await _authService.LoginByVerificationCodesAsync(request.FirstCode, request.SecondCode);
        return string.IsNullOrWhiteSpace(token) ? Unauthorized("Invalid codes") : Ok(new { Token = token });
    }

    [HttpPost("login-admin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginCodesRequest request)
    {
        return Ok();
    }
}