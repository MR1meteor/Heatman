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
        if (url == null) return BadRequest("Файл не найден");
        return Ok(url);
    }

    [HttpGet("{fileName}/base64")]
    public async Task<IActionResult> GetFileAsBase64([FromRoute] string fileName)
    {
        var base64 = await _uploadService.GetFileAsBase64Async(fileName);

        if (base64 == null)
        {
            return BadRequest("File not found");
        }

        return Ok(base64);
    }
}