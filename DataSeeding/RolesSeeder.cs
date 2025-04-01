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
                    var newRole = new IdentityRole(role);
                     await _roleManager.CreateAsync(newRole);
                }
            }
        }
    }
}
