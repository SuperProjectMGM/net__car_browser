using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace search.api.Interfaces;

public interface IAuthInterface
{
    public Task<IdentityResult> CreateNewUser(RegisterModel model);
    public JwtSecurityToken GetToken(List<Claim> authClaims);
}