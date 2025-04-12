using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RequestService.Controllers;

public class BaseController : ControllerBase, IActionFilter
{
    public string User1Id { get; private set; } = string.Empty;
    public string User2Id { get; private set; } = string.Empty;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext?.User == null)
        {
            return;
        }
        
        User1Id = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("User1Id", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
        User2Id = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals("User2Id", StringComparison.OrdinalIgnoreCase))?.Value ?? string.Empty;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}