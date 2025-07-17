using DigitalDevices.AuthService.Core.Interfaces;
using DigitalDevices.AuthService.Infrastructure.Authentication;
using DigitalDevices.AuthService.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalDevices.AuthService.Infrastructure.Extensions
{
    public static class Implementations
    {
        public static void AddImplementations(this IServiceCollection services)
        {

            services.AddScoped<IAuthRepo, AuthRepo>();

            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<IJWTProvider, JWTProvider>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
    }
}
