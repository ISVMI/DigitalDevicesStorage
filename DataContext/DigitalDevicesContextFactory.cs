using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.DataContext
{
    public class DigitalDevicesContextFactory:IDbContextFactory<DigitalDevicesContext>
    {
        private readonly IConfiguration _configuration;

        public DigitalDevicesContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DigitalDevicesContext CreateDbContext()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<DigitalDevicesContext>()
                .UseSqlServer(connectionString);

            return new DigitalDevicesContext(optionsBuilder.Options);
        }
    }
}
