using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using search.api.Models;

public class UserFieldsHandler : AuthorizationHandler<UserFieldsRequirement>
{
    private readonly UserManager<UserDetails> _userManager;

    public UserFieldsHandler(UserManager<UserDetails> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserFieldsRequirement requirement)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Fail();
            return;
        }

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            context.Fail();
            return;
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            context.Fail();
            return;
        }

        if (!string.IsNullOrEmpty(user.Name) &&
            !string.IsNullOrEmpty(user.Surname) &&
            user.BirthDate != default &&
            !string.IsNullOrEmpty(user.DrivingLicenseNumber) &&
            user.DrivingLicenseIssueDate != default &&
            user.DrivingLicenseExpirationDate > DateTime.Now)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}