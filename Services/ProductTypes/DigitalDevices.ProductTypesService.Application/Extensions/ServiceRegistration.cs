using System.Reflection;
using DigitalDevices.ProductTypesService.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using DigitalDevices.ProductTypesService.Application.Interfaces;

namespace DigitalDevices.ProductTypesService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductTypesService, Services.ProductTypesService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateProductTypeHandler).Assembly,
                typeof(DeleteProductTypeHandler).Assembly,
                typeof(GetProductTypeHandler).Assembly,
                typeof(GetAllProductTypesHandler).Assembly
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
