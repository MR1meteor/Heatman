using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/ping")]
public class PingController : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> Ping()
    {
        return Ok("pong");
    }
}