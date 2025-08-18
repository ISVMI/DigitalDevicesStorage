using Shared.Extensions;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var authority = builder.Configuration.GetSection("AuthAuthority").Value;

builder.Services.AddJwtExtensions(authority, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

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

app.MapReverseProxy(proxyPipeline =>
{
    proxyPipeline.Use(async (context, next) =>
    {
        Console.WriteLine($"--> Received request: {context.Request.Path}");
        await next();
        Console.WriteLine($"--> Response status: {context.Response.StatusCode}");
    });
});

app.Run();
