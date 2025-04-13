using Microsoft.AspNetCore.Mvc;
using ReportService.Models.Dtos;
using ReportService.Services.Interfaces;
using Shared.Controllers;

namespace ReportService.Controllers;

[ApiController]
[Route("api/report")]
public class ReportController : BaseController
{
    private readonly IActsService _actsService;

    public ReportController(IActsService actsService)
    {
        _actsService = actsService;
    }
    
    [HttpPost("control-act")]
    public async Task<IActionResult> CreateReport([FromBody] CreateControlActRequest request)
    {
        var response = await _actsService.CreateControlActAsync(request);
        return response ? Ok() : BadRequest("Failed to create control report");
    }

    [HttpPost("stop-resume-act")]
    public async Task<IActionResult> CreateStopResumeAct([FromBody] CreateStopResumeActRequest request)
    {
        var response = await _actsService.CreateStopResumeActAsync(request);
        return response ? Ok() : BadRequest("Failed to create stop resume act");
    }

    [HttpGet("control-act/{requestId:guid}")]
    public async Task<IActionResult> GetControlAct(Guid requestId)
    {
        var response = await _actsService.GetControlActAsync(requestId);
        return !string.IsNullOrWhiteSpace(response) ? Ok() : BadRequest("Failed to get control act");
    }
}