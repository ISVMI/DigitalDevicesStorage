using DigitalDevices.AuthService.Core.Interfaces;
using DigitalDevices.AuthService.Infrastructure.Authentication;
using DigitalDevices.AuthService.Infrastructure.Data;
using DigitalDevices.AuthService.Infrastructure.DataSeeding;
using DigitalDevices.AuthService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace DigitalDevices.AuthService.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AuthContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAuthRepo, AuthRepo>();

            services.AddScoped<IJWTProvider, JWTProvider>();

            services.AddScoped<IPasswordHasher, PasswordHasher>();
        }
        public static async Task AddDatabaseInitialization(this IServiceProvider services)
        {
            await services.InitializeDatabaseAsync<AuthContext>(DbInitializer.InitializeAsync);
        }
    }
}
