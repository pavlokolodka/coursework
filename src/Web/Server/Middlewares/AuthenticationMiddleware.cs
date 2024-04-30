using ReserveSpot.Domain;
using System.Security.Claims;

namespace Web.Server.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserService _userService;

        public AuthenticationMiddleware(RequestDelegate next, UserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    var user = _userService.FindOneById(userId);
                    if (user == null)
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Authentication has failed");
                        return;
                    }

                    context.Items["IsAdmin"] = user.IsAdmin;
                }
                else
                {
                    context.Response.StatusCode = 401; 
                    await context.Response.WriteAsync("Authentication has failed");
                    return; 
                }
            }
            await _next(context);
        }
    }

}
