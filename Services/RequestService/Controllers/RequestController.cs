using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RequestService.Mapping;
using RequestService.Models.Dtos;
using RequestService.Services.Interfaces;
using Shared.Controllers;

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

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetByToken()
    {
        var response = await _requestService.GetPersonalAsync();
        
        return Ok(response.MapToDto());
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

    [HttpPost("by-excel")]
    public async Task<IActionResult> CreateByExcel([FromBody] UploadFileRequest request)
    {
        var response = await _requestService.CreateByExcelFileAsync(request.FileBytes);

        return Ok(response);
    }

    [HttpPut("{requestId:guid}/set-complete")]
    public async Task<IActionResult> SetCompleteStatus([FromRoute] Guid requestId)
    {
        var response = await _requestService.SetCompletedStatusAsync(requestId);
        return Ok(response);
    }

    [HttpPost("{requestId:guid}/before-photo")]
    public async Task<IActionResult> UploadBeforePhoto([FromRoute] Guid requestId, [FromBody] UploadFileRequest request)
    {
        var isSuccess = await _requestService.UploadBeforeFileAsync(requestId, request.FileBytes);
        
        return isSuccess ? Ok() : BadRequest("File not uploaded");
    }

    [HttpPost("{requestId:guid}/after-photo")]
    public async Task<IActionResult> UploadAfterPhoto([FromRoute] Guid requestId, [FromBody] UploadFileRequest request)
    {
        var isSuccess = await _requestService.UploadAfterFileAsync(requestId, request.FileBytes);
        
        return isSuccess ? Ok() : BadRequest("File not uploaded");
    }
}