namespace DigitalDevices.AuthService.Core.Interfaces
{
    public interface IUsersService
    {
        Task<string> Login(string username, string password);
        Task Register(string username, string password, string? code);
    }
}