using Microsoft.AspNetCore.Authorization;

namespace DigitalDevices.AuthService.Api.Authorization
{
    public class MinimumPermissionLevelRequirement : IAuthorizationRequirement
    {
        public int RequiredLevel { get; }
        public MinimumPermissionLevelRequirement(int requiredLevel)
        {
            RequiredLevel = requiredLevel;
        }
    }
}
