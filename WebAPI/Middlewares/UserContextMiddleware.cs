using System.Security.Claims;

namespace WebAPI.Middlewares;

public class UserContextMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            context.Items["UserId"] = userId;
        }
        await _next(context);
    }
}
