using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.DataSeeding
{
    public class RolesSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRolesAsync()
        {

            string[] roles = new[] { "Admin", "Manager", "User" };

            if (!await _roleManager.Roles.AnyAsync())
            {
                foreach (var role in roles)
                {
                     await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
