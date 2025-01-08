using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.IdentityModel.Tokens;
using search.api.Interfaces;
using search.api.Models;

public class AuthRepository : IAuthInterface
{
    private readonly UserManager<UserDetails> _userManager;
    private readonly IConfiguration _configuration;

    public AuthRepository(UserManager<UserDetails> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<bool> CheckIfUserExist(UserDetails details)
    {
        var user = await _userManager.FindByEmailAsync(details.Email!);
        return !(user is null);
    }

    public async Task<IdentityResult> CreateNewUser(RegisterModel model, UserDetails details)
    {
        var result = await _userManager.CreateAsync(details, model.Password);
        return result;
    }

    public JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]!));
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT_ISSUER"],
            audience: _configuration["JWT_AUDIENCE"],
            expires: DateTime.Now.AddMinutes(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
        return token;
    }

    public async Task<List<Claim>> ReturnClaims(UserDetails user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName ?? ""),
            new Claim(ClaimTypes.Email, user.Email ?? ""),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
        return  claims; 
    }

    public async Task<List<Claim>?> CheckLogin(LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);
        if (user is null)
            return null;
        
        var isVaPassValid = await _userManager.CheckPasswordAsync(user, model.Password!);
        if (!isVaPassValid)
            return null;
        
        var claims = await ReturnClaims(user);
        return claims;
    }

    public AuthenticationProperties AddGoogleOptions()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = _configuration["GOOGLE_AUTH_REDIRECT_URI"]
        };
        return properties;
    }

    // Here we already know that user is authenticated by google, and only should 
    // find if he has account in our system/
    // I assume, that a person tha is signin in with google, will be signed in with google always 
    // I'll generate random password
    public async Task<List<Claim>?> GoogleCallbackFunction(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        // Registration of new user
        UserDetails userTmp;
        if (user is null)
        {
            userTmp = new UserDetails();
            string password = RandomPassword(12);
            userTmp.Email = email;
            // TODO: Change temporary solution
            userTmp.UserName = email;
            var result = await _userManager.CreateAsync(userTmp, password);
            if (!result.Succeeded)
            {
                return null;
            }    
            userTmp = await _userManager.FindByEmailAsync(email);
        }
        else
        {
            userTmp = user;
        }
        return await ReturnClaims(userTmp!);
    }

    private static Random rand = new Random();

public static string RandomPassword(int length = 8)
{
    string[] categories = {
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ",
        "abcdefghijklmnopqrstuvwxyz",
        "!-_*+&$",
        "0123456789" };

    List<char> chars = new List<char>(length);

    // add one char from each category
    foreach(string cat in categories)
    {
        chars.Add(cat[rand.Next(cat.Length)]);
    }

    // add random chars from any category until we hit the length
    string all = string.Concat(categories);            
    while (chars.Count < length)
    {
        chars.Add(all[rand.Next(all.Length)]);
    }

    // shuffle and return our password
    var shuffled = chars.OrderBy(c => rand.NextDouble()).ToArray();
    return new string(shuffled);
}
}