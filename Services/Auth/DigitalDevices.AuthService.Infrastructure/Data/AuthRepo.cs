using DigitalDevices.AuthService.Core.Interfaces;
using DigitalDevices.AuthService.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalDevices.AuthService.Infrastructure.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AuthContext _context;

        public AuthRepo(AuthContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user, string? code, CancellationToken token = default)
        {
            try
            {
                UserAddingLogic(user, code);

                await _context.Users.AddAsync(user, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not add a new User: {ex.Message}");
            }
        }

        public async Task<User?> FindUser(string username)
        {
            if (!_context.Users.Any())
            {
                return null;
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x => x.Username == username);

            return user;
        }

        public async Task<List<Role>> GetRoles()
        {
            if (!_context.Roles.Any())
            {
                return null;
            }

            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        public async Task SaveChanges(CancellationToken token = default)
        {
            await _context.SaveChangesAsync(token);
        }

        private void UserAddingLogic(User user, string? code)
        {
            var existingUser = _context.Users
                   .Where(u => u.Username == user.Username)
                   .ToList()
                   .FirstOrDefault();

            if (existingUser is null)
            {
                var userRole = _context.Roles
                    .Where(r => r.SecretCode == code).ToList().FirstOrDefault();

                if (userRole == null)
                {
                    userRole = _context.Roles
                        .Where(r => r.PermissionLevel == 1)
                        .ToList()
                        .FirstOrDefault(); //Set role as "User"

                    if (userRole == null) // Set a default "User" role for user
                    {
                        userRole = new Role
                        {
                            Name = "User",
                            PermissionLevel = 1
                        };

                        _context.Roles.Add(userRole);
                    }
                }

                user.RoleId = userRole.Id;
                user.Role = userRole; //Set a role for user

                userRole.Users.Add(user);
            }
            else
            {
                throw new Exception(message: "--> User already Exists!");
            }
        }
    }
}
