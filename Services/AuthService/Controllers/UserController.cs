using AuthService.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace AuthService.Controllers;

[ApiController]
[Route("api/auth/user")]
public class UserController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
    {
        return Ok();
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        return Ok();
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok();
    }

    [HttpGet("personal-is-admin")]
    public async Task<IActionResult> GetPersonalIsAdmin()
    {
        return Ok();
    }
}