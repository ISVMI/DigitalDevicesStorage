using DigitalDevices.AuthService.Core.Models;

namespace DigitalDevices.AuthService.Core.Interfaces
{
    public interface IAuthRepo
    {
        Task AddUser(User user, string? code, CancellationToken token = default);
        Task SaveChanges(CancellationToken token = default);
        Task<User?> FindUser(string username);

        Task<List<Role>> GetRoles();
    }
}
