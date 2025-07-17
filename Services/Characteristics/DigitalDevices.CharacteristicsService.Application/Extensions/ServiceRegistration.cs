using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DigitalDevices.CharacteristicsService.Application.Handlers;
using DigitalDevices.CharacteristicsService.Application.Interfaces;

namespace DigitalDevices.CharacteristicsService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICharacteristicsService, Services.CharacteristicsService>();
            services.AddScoped<ICharacteristicsTypeService, Services.CharacteristicsTypeService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateCharacteristicHandler).Assembly,
                typeof(DeleteCharacteristicHandler).Assembly,
                typeof(GetCharacteristicHandler).Assembly,
                typeof(GetAllCharacteristicsHandler).Assembly,
                typeof(CreateCharacteristicTypeHandler).Assembly,
                typeof(DeleteCharacteristicTypeHandler).Assembly,
                typeof(GetCharacteristicTypeHandler).Assembly,
                typeof(GetAllCharacteristicsTypesHandler).Assembly
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
