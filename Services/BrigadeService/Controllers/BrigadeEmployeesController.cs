using BrigadeService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace BrigadeService.Controllers;

[ApiController]
[Route("api/brigade/employees")]
public class BrigadeEmployeesController : BaseController
{
    private readonly IBrigadeEmployeesService _brigadeEmployeesService;

    public BrigadeEmployeesController(IBrigadeEmployeesService brigadeEmployeesService)
    {
        _brigadeEmployeesService = brigadeEmployeesService;
    }
    
    [HttpGet("by-brigade/{brigadeId:guid}/ids")]
    public async Task<IActionResult> GetBrigadeEmployees(Guid brigadeId)
    {
        return Ok(await _brigadeEmployeesService.GetEmployeesIds(brigadeId));
    }
}