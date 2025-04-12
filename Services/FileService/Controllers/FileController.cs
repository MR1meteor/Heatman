using FileService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace FileService.Controllers;

[ApiController]
[Route("api/file")]
public class FileController : BaseController
{
    private readonly IFileService _uploadService;

    public FileController(IFileService uploadService)
    {
        _uploadService = uploadService;
    }

    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл пустой");

        var filename = await _uploadService.UploadFileAsync(file);
        return Ok(new { FileName = filename });
    }

    [HttpGet("url")]
    public async Task<IActionResult> GetUrl([FromQuery] string filename)
    {
        var url = await _uploadService.GetFileUrlAsync(filename);
        if (url == null) return NotFound("Файл не найден");
        return Ok(new { Url = url });
    }
}