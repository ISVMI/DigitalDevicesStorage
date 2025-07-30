using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using DigitalDevices.CharacteristicsService.Application.Handlers;

namespace DigitalDevices.CharacteristicsService.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateCharacteristicHandler).Assembly,
                typeof(CreateCharacteristicTypeHandler).Assembly,
                typeof(EditCharacteristicHandler).Assembly,
                typeof(EditCharacteristicTypeHandler).Assembly,
                typeof(DeleteCharacteristicHandler).Assembly,
                typeof(DeleteCharacteristicTypeHandler).Assembly,
                typeof(GetAllCharacteristicsHandler).Assembly,
                typeof(GetAllCharacteristicsTypesHandler).Assembly,
                typeof(GetCharacteristicsHandler).Assembly,
                typeof(GetCharacteristicsTypesHandler).Assembly,
                typeof(GetCharacteristicHandler).Assembly,
                typeof(GetCharacteristicTypeHandler).Assembly,
                typeof(GetCharacteristicsByProductTypeHandler).Assembly
            };

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

            return services;
        }
    }
}
