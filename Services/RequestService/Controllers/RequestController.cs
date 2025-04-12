﻿using Microsoft.AspNetCore.Mvc;
using RequestService.Models.Dtos;
using RequestService.Services.Interfaces;

namespace RequestService.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestController : BaseController
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }
    
    [HttpGet("{brigadeId:guid}")]
    public async Task<IActionResult> GetByBrigade([FromRoute] Guid brigadeId)
    {
        var response = await _requestService.GetByBrigadeAsync(brigadeId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateNewRequest request)
    {
        var response = await _requestService.CreateAsync(request);
        return Ok(response);
    }

    [HttpPut("set-complete/{requestId:guid}")]
    public async Task<IActionResult> SetCompleteStatus([FromRoute] Guid requestId)
    {
        var response = await _requestService.SetCompletedStatusAsync(requestId);
        return Ok(response);
    }
}