using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[ApiController]
[Route("gateway")]
public class PingController : ControllerBase
{
    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        return Ok("pong");
    }
}