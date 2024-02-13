using System.Security.Claims;

namespace AuthService.Api.Middlewares;

public class InjectUserContextMiddleware
{
    private readonly RequestDelegate _next;

    public InjectUserContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        await _next(context);
        
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userRoles = context.User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

        context.Response.Headers.Append("X-User-Id", userId);
        context.Response.Headers.Append("X-User-Roles", string.Join(" ", userRoles));
    }
}
