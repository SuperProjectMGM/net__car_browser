using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using search.api.Models;

namespace search.api.Interfaces;

public interface IAuthInterface
{
    public Task<bool> CheckIfUserExist(UserDetails details);
    public Task<IdentityResult> CreateNewUser(RegisterModel model, UserDetails details);
    public JwtSecurityToken GetToken(List<Claim> authClaims);
    public Task<List<Claim>?> CheckLogin(LoginModel model);
    public Task<List<Claim>> ReturnClaims(UserDetails user);
    public AuthenticationProperties AddGoogleOptions();
    public Task<List<Claim>?> GoogleCallbackFunction(string email);
}