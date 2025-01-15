using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using search.api.Interfaces;
using search.api.Models;

public class UserRepository : IUserInfoInterface
{
    private readonly AuthDbContext _context;
    private readonly IConfiguration _configuration;
    public UserRepository(IConfiguration configuration, AuthDbContext context)
    {
        _context = context;
        _configuration = configuration;
    }
    public async Task<int> ChangeDataAboutUser()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<UserDetails?> FindUserById(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        return user;
    }

    public int? ReturnIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]!);
        var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidIssuer = _configuration["JWT_ISSUER"],
            ValidAudience = _configuration["JWT_AUDIENCE"]
        }, out SecurityToken validatedToken);
        var id = int.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        return id;
    }
}