using Shared.Extensions;
using System.Security.Claims;
using DigitalDevices.ApiGateway.Api.Extensions;

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

app.UseGatewayAuthorization();

app.UseClaimsPropagation();

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
