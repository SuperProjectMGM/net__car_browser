using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using search.api.Models;

public class DrivingLicenseRequirement : IAuthorizationRequirement
{
}

public class DrivingLicenseRequirementHandler : AuthorizationHandler<DrivingLicenseRequirement>
{
    private readonly UserManager<UserDetails> _userManager;

    public DrivingLicenseRequirementHandler(UserManager<UserDetails> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DrivingLicenseRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            var userEmail = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail != null)
            {
                var user = await _userManager.FindByIdAsync(userEmail);
                if (user != null && !string.IsNullOrEmpty(user.DrivingLicenseNumber) &&
                    !string.IsNullOrEmpty(user.DrivingLicenseIssueDate) &&
                    !string.IsNullOrEmpty(user.DrivingLicenseExpirationDate) && 
                    (DateTime.Parse(user.DrivingLicenseExpirationDate).CompareTo(DateTime.Now) == 1))  // Sprawdzamy czy walidne dane
                {
                    context.Succeed(requirement);
                }
            }
        }
    }
}