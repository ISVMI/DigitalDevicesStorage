using DigitalDevices.ProductTypesService.Core.Interfaces;
using DigitalDevices.ProductTypesService.Infrastructure.Data;
using DigitalDevices.ProductTypesService.Infrastructure.DataSeeding;
using DigitalDevices.ProductTypesService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace DigitalDevices.ProductTypesService.Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseService<ProductTypesContext>(configuration);
            services.AddScoped<IProductTypesRepo, ProductTypesRepo>();
        }

        public static async Task AddDatabaseInitialization(this IServiceProvider services)
        {
            await services.InitializeDatabaseAsync<ProductTypesContext>(DbInitializer.InitializeAsync);
        }
    }
}
