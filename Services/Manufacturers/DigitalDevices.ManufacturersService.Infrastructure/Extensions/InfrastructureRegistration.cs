using DigitalDevices.ManufacturersService.Core.Interfaces;
using DigitalDevices.ManufacturersService.Infrastructure.Data;
using DigitalDevices.ManufacturersService.Infrastructure.DataSeeding;
using DigitalDevices.ManufacturersService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace DigitalDevices.ManufacturersService.Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseService<ManufacturersContext>(configuration);
            services.AddScoped<IManufacturersRepo, ManufacturersRepo>();
        }

        public static async Task AddDatabaseInitialization(this IServiceProvider services)
        {
            await services.InitializeDatabaseAsync<ManufacturersContext>(DbInitializer.InitializeAsync);
        }
    }
}
