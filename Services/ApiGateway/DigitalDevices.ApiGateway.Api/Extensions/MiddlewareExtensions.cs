using System.Security.Claims;

namespace DigitalDevices.ApiGateway.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseGatewayAuthorization(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var path = context.Request.Path.Value?.ToLowerInvariant() ?? "";
                var method = context.Request.Method;
                int.TryParse(context.User.FindFirst("PermissionLevel")?.Value, out var permissionLevel);

                if (path.StartsWith("/api/auth"))
                {
                    if (path != "/api/auth/login" && path != "/api/auth/register")
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }

                if (path.StartsWith("/api/products") && method == HttpMethods.Get)
                {
                    await next();
                    return;
                }

                if (path.StartsWith("/api/products") &&
                    (method == HttpMethods.Post || method == HttpMethods.Put || method == HttpMethods.Delete))
                {
                    if (!context.User.Identity?.IsAuthenticated ?? true)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }

                    await next();
                    return;
                }

                if (path.StartsWith("/api/manufacturers") || path.StartsWith("/api/producttypes") ||
                    path.StartsWith("/api/characteristics"))
                {
                    if ((path.StartsWith("/api/manufacturers/all") || path.StartsWith("/api/manufacturers/paged")) && method == HttpMethods.Get)
                    {
                        await next();
                        return;
                    }

                    if (!context.User.Identity?.IsAuthenticated ?? true)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return;
                    }

                    if (permissionLevel < 4 || !(context.User.IsInRole("Manager") || context.User.IsInRole("Admin")))
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }

                    await next();
                    return;
                }

                await next();
            });
        }

        public static void UseClaimsPropagation(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                if (context.User?.Identity?.IsAuthenticated == true)
                {
                    var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    var roles = string.Join(",", context.User.FindAll(ClaimTypes.Role).Select(c => c.Value));
                    var permissionLevel = context.User.FindFirst("PermissionLevel")?.Value;

                    if (!string.IsNullOrEmpty(userId)) context.Request.Headers["X-User-Id"] = userId;
                    if (!string.IsNullOrEmpty(roles)) context.Request.Headers["X-User-Roles"] = roles;
                    if (!string.IsNullOrEmpty(permissionLevel)) context.Request.Headers["X-Permission-Level"] = permissionLevel;
                }
                await next();
            });
        }
    }
}
