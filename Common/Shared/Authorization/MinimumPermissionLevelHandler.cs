using Microsoft.AspNetCore.Authorization;

namespace DigitalDevices.AuthService.Api.Authorization
{
    public class MinimumPermissionLevelHandler : AuthorizationHandler<MinimumPermissionLevelRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            MinimumPermissionLevelRequirement requirement)
        {
            var claim = context.User.FindFirst(c => c.Type == "PermissionLevel");
            if (claim == null || !int.TryParse(claim.Value, out var userLevel))
                return Task.CompletedTask;

            if (userLevel >= requirement.RequiredLevel)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
