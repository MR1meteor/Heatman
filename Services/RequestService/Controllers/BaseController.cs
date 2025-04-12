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
        
        User1Id = HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToUpper().Equals("USER1ID"))?.Value ?? string.Empty;
        User2Id = HttpContext.User.Claims.FirstOrDefault(c => c.Type.ToUpper().Equals("USER2ID"))?.Value ?? string.Empty;
    }
}