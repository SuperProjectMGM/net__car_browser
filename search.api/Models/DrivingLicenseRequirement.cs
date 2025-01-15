using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
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
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null && !string.IsNullOrEmpty(user.DrivingLicenseNumber) &&
                    (user.DrivingLicenseIssueDate != default) &&
                    (user.DrivingLicenseExpirationDate != default) && 
                    (user.DrivingLicenseExpirationDate.CompareTo(DateTime.Now) == 1))
                {
                    context.Succeed(requirement);
                    return;
                }
            }
        }

        var httpContext = context.Resource as Microsoft.AspNetCore.Http.HttpContext;
        if (httpContext != null)
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            await httpContext.Response.WriteAsync("User must update driving license information.");
        }
        context.Fail(); 
    }
}