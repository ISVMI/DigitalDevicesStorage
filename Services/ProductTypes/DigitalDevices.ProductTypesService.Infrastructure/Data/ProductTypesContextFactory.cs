using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DigitalDevices.ProductTypesService.Infrastructure.Data
{
    public class ProductTypesContextFactory : IDesignTimeDbContextFactory<ProductTypesContext>
    {
        public ProductTypesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductTypesContext>();
            var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            return new ProductTypesContext(optionsBuilder.Options);
        }
    }
}
