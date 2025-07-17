using DigitalDevices.AuthService.Api.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DigitalDevices.AuthService.Api.Extensions
{
    public static class AuthExtensions
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(
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
}
