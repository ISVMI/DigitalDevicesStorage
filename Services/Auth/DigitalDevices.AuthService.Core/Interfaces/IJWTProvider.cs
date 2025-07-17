using DigitalDevices.AuthService.Core.Models;

namespace DigitalDevices.AuthService.Core.Interfaces
{
    public interface IJWTProvider
    {
        string GenerateToken(User user);
    }
}