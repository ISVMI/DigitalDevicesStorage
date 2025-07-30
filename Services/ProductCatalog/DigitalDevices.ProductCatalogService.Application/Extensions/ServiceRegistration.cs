using System.Reflection;
using DigitalDevices.ProductCatalogService.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalDevices.ProductCatalogService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateProductHandler).Assembly,
                typeof(EditProductHandler).Assembly,
                typeof(DeleteProductHandler).Assembly,
                typeof(GetProductHandler).Assembly,
                typeof(GetAllProductsPagedHandler).Assembly,
                typeof(GetAllProductsHandler).Assembly
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
