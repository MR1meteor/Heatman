using Microsoft.AspNetCore.Mvc;

namespace RequestService.Controllers;

public class BaseController : ControllerBase
{
    public string User1Id { get; private set; } = string.Empty;
    public string User2Id { get; private set; } = string.Empty;

    protected BaseController()
    {
        if (HttpContext?.User == null)
        {
            return;
        }
        
        User1Id = HttpContext.User.FindFirst("User1Id")?.Value ?? string.Empty;
        User2Id = HttpContext.User.FindFirst("User2Id")?.Value ?? string.Empty;
    }
}