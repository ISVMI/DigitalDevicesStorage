using System.Security.Authentication;
using DigitalDevices.AuthService.Core.Interfaces;
using DigitalDevices.AuthService.Core.Models;

namespace DigitalDevices.AuthService.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthRepo _repo;
        private readonly IJWTProvider _jwtProvider;

        public UsersService(
            IPasswordHasher passwordHasher,
            IAuthRepo repo,
            IJWTProvider jwtProvider)
        {
            _passwordHasher = passwordHasher;
            _repo = repo;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _repo.FindUser(username);

            if (user == null)
            {
                throw new InvalidCredentialException("User is not registered!");
            }
            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new InvalidCredentialException("Password was incorrect!");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        public async Task Register(string username, string password, string? code)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var user = new User()
            {
                Username = username,
                PasswordHash = hashedPassword
            };

            await _repo.AddUser(user, code);
            await _repo.SaveChanges();
        }
    }
}
