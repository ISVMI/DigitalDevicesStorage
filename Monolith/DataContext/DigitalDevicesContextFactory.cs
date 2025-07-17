using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DigitalDevices.DataContext
{
    public class DigitalDevicesContextFactory : IDesignTimeDbContextFactory<DigitalDevicesContext>
    {
        public DigitalDevicesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DigitalDevicesContext>();
            optionsBuilder.UseSqlServer("Server=(local);Database=DigitalDevices;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

            return new DigitalDevicesContext(optionsBuilder.Options);
        }
    }
}
