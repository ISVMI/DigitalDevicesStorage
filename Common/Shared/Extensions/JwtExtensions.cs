using DigitalDevices.AuthService.Api.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.Extensions;

public static class JwtExtensions
{
    /// <summary>
    /// Jwt authentication logic with cookie and policies
    /// </summary>
    /// <param name="services"></param>
    /// <param name="authority"></param>
    /// <param name="configuration"></param>
    public static void AddJwtExtensions(this IServiceCollection services, string authority, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "AuthService",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["some-cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy =>
                policy.Requirements.Add(new MinimumPermissionLevelRequirement(PermissionLevels.Admin)))

            .AddPolicy("ManagerPolicy", policy =>
                policy.Requirements.Add(new MinimumPermissionLevelRequirement(PermissionLevels.Manager)))

            .AddPolicy("UserPolicy", policy =>
                policy.Requirements.Add(new MinimumPermissionLevelRequirement(PermissionLevels.User)));

        services.AddSingleton<IAuthorizationHandler, MinimumPermissionLevelHandler>();
    }

    /// <summary>
    /// Version without authority
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddJwtExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "AuthService",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration.GetSection("JwtOptions")
                            .GetSection("SecretKey")
                            .Value))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["some-cookies"];

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy("AdminPolicy", policy =>
                policy.Requirements.Add(new MinimumPermissionLevelRequirement(PermissionLevels.Admin)))

            .AddPolicy("ManagerPolicy", policy =>
                policy.Requirements.Add(new MinimumPermissionLevelRequirement(PermissionLevels.Manager)))

            .AddPolicy("UserPolicy", policy =>
                policy.Requirements.Add(new MinimumPermissionLevelRequirement(PermissionLevels.User)));

        services.AddSingleton<IAuthorizationHandler, MinimumPermissionLevelHandler>();
    }
}