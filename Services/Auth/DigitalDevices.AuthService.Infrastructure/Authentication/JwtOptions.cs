namespace DigitalDevices.AuthService.Infrastructure.Authentication
{
    public class JwtOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int ExpirationHours { get; set; }
    }
}