using DigitalDevices.ProductCatalogService.Core.Interfaces;
using DigitalDevices.ProductCatalogService.Infrastructure.Data;
using DigitalDevices.ProductCatalogService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;

namespace DigitalDevices.ProductCatalogService.Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseService<ProductCatalogContext>(configuration);
            services.AddScoped<IProductCatalogRepo, ProductCatalogRepo>();
        }
    }
}
