using DigitalDevices.CharacteristicsService.Core.Models;
using DigitalDevices.CharacteristicsService.Infrastructure.Data;
using System.Reflection;
using System.Text.Json;

namespace DigitalDevices.CharacteristicsService.Infrastructure.DataSeeding
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(CharacteristicsContext context)
        {
            if (context.CharacteristicsType.Any())
            {
                return;
            }

            var assembly = Assembly.GetExecutingAssembly();

            var resourceName = GetResourceName("CharacteristicTypes.json");

            await using var stream = assembly.GetManifestResourceStream(resourceName);

            using var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();

            var characteristicsTypes = JsonSerializer.Deserialize<CharacteristicsType[]>(json);

            await context.CharacteristicsType.AddRangeAsync(characteristicsTypes);

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
