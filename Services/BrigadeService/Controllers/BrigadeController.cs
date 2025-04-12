using System.Text.Json;
using BrigadeService.Models.Requests;
using BrigadeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace BrigadeService.Controllers;

[ApiController]
[Route("api/brigade")]
public class BrigadeController : BaseController
{
    private readonly IBrigadeService _brigadeService;

    public BrigadeController(IBrigadeService brigadeService)
    {
        _brigadeService = brigadeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateToday([FromBody] CreateBrigadeRequest request)
    {
        var result = await _brigadeService.CreateTodayAsync(request.FirstUserId, request.SecondUserId);

        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("today/id/personal")]
    public async Task<IActionResult> GetPersonalId()
    {
        var result = await _brigadeService.GetIdByUserIds(FirstUserId, SecondUserId);
        
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("today/id/by-users")]
    public async Task<IActionResult> GetTodayByUsers([FromQuery] Guid firstUserId, [FromQuery] Guid secondUserId)
    {
        var result = await _brigadeService.GetIdByUserIds(firstUserId.ToString(), secondUserId.ToString());
        
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }
}