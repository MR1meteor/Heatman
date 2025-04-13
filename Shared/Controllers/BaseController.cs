using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Controllers;

public class BaseController : ControllerBase, IActionFilter
{
    protected string FirstUserId { get; private set; } = string.Empty;
    protected string SecondUserId { get; private set; } = string.Empty;

    [NonAction]
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext?.User == null)
        {
            return;
        }
        
        FirstUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("FirstUserId", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
        SecondUserId = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("SecondUserId", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
    }

    [NonAction]
    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}