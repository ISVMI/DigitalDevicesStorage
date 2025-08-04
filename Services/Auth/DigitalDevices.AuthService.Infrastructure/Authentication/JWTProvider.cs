using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DigitalDevices.AuthService.Core.Interfaces;
using DigitalDevices.AuthService.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DigitalDevices.AuthService.Infrastructure.Authentication
{
    public class JWTProvider : IJWTProvider
    {
        private readonly JwtOptions _options;

        public JWTProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            string role = user.Role.PermissionLevel switch
            {
                > 4 => "Admin",
                4 => "Manager",
                _ => "User"
            };

            claims.Add(new Claim(ClaimTypes.Role, role));

            claims.Add(new Claim("PermissionLevel", user.Role.PermissionLevel.ToString()));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AuthService",
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(_options.ExpirationHours));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
