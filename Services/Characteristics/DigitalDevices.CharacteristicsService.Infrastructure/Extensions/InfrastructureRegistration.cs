using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DigitalDevices.CharacteristicsService.Core.Interfaces;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using DigitalDevices.CharacteristicsService.Infrastructure.DataSeeding;
using DigitalDevices.CharacteristicsService.Infrastructure.Repositories;
using Shared.Extensions;

namespace DigitalDevices.CharacteristicsService.Infrastructure.Extensions
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabaseService<CharacteristicsContext>(configuration);
            services.AddScoped<ICharacteristicsRepo, CharacteristicsRepo>();
            services.AddScoped<ICharacteristicsTypeRepo, CharacteristicsTypeRepo>();
        }

        public static async Task AddDatabaseInitialization(this IServiceProvider services)
        {
            await services.InitializeDatabaseAsync<CharacteristicsContext>(DbInitializer.InitializeAsync);
        }
    }
}
