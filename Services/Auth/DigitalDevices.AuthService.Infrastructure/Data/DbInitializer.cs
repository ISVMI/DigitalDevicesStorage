using DigitalDevices.AuthService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.AuthService.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(AuthContext context)
        {
            if (context == null)
            {
                Console.WriteLine($"--> Database context was null");
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Roles.Any())
            {
                Console.WriteLine("--> Seeding Data...");

                context.Roles.AddRange(
                    new Role() { Name = "User", PermissionLevel = 1, SecretCode = null },
                    new Role() { Name = "Admin", PermissionLevel = 5, SecretCode = "ClearanceLevelO5" },
                    new Role() { Name = "Manager", PermissionLevel = 4, SecretCode = "ClearanceLevel4&Below" }
                );

                await context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("--> We already have data");
            }
        }
    }
}