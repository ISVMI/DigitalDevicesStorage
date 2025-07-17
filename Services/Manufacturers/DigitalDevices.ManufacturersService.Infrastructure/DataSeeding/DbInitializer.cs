using DigitalDevices.ManufacturersService.Core.Models;
using DigitalDevices.ManufacturersService.Infrastructure.Data;
using System.Reflection;
using System.Text.Json;

namespace DigitalDevices.ManufacturersService.Infrastructure.DataSeeding
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ManufacturersContext context)
        {
            if (context.Manufacturers.Any())
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = GetResourceName("Manufacturers.json");

            await using var stream = assembly.GetManifestResourceStream(resourceName);

            using var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();

            var manufacturers = JsonSerializer.Deserialize<Manufacturer[]>(json);

            await context.Manufacturers.AddRangeAsync(manufacturers);

            await context.SaveChangesAsync();
        }

        private static string GetResourceName(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = resourceNames.FirstOrDefault(r => r.EndsWith(fileName));

            if (resourceName == null)
                throw new FileNotFoundException($"Resource '{fileName}' not found.");

            return resourceName;
        }
    }
}
