using DigitalDevices.AuthService.Application.Services;
using DigitalDevices.AuthService.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalDevices.AuthService.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
