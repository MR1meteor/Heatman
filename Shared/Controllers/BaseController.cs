using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Controllers;

public class BaseController : ControllerBase, IActionFilter
{
    public string FirstUserId { get; private set; } = string.Empty;
    public string SecondUserId { get; private set; } = string.Empty;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext?.User == null)
        {
            return;
        }
        
        FirstUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("FirstUserId", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
        SecondUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("SecondUserId", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}