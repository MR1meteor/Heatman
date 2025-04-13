using Microsoft.AspNetCore.Mvc;
using ReportService.Models.Dtos;
using Shared.Controllers;

namespace ReportService.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : BaseController 
{
    [HttpPost("control-act")]
    public async Task<IActionResult> CreateReport([FromBody] CreateControlActRequest request)
    {
        return Ok();
    }

    [HttpPost("stop-resume-act")]
    public async Task<IActionResult> CreateStopResumeAct([FromBody] CreateStopResumeActRequest request)
    {
        return Ok();
    }
}