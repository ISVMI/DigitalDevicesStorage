using System.Reflection;
using DigitalDevices.ManufacturersService.Application.Handlers;
using DigitalDevices.ManufacturersService.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalDevices.ManufacturersService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IManufacturersService, Services.ManufacturersService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateManufacturerHandler).Assembly,
                typeof(DeleteManufacturerHandler).Assembly,
                typeof(GetManufacturerHandler).Assembly,
                typeof(GetAllManufacturersHandler).Assembly
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
