using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using search.api.Models;

namespace search.api.Controllers;


[Route("search.api/[controller]")]
[ApiController]
public class UserInfoController: ControllerBase
{
    // WAZNE TODO: Przerobic na Repositories for interface i injektowaC. Tak samo w authentykacji
    private readonly AuthDbContext _context;
    private readonly UserManager<UserDetails> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    public UserInfoController(UserManager<UserDetails> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AuthDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _context = context;
    }

    [HttpGet]
    [Route("user-info")]
    public async Task<IActionResult> UserInfo([FromQuery] string token)
    {
        // TODO: Przerobic na jakas metode zamiast tego, bo juz po raz 3 to powtarzam
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]);
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
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            return NotFound("Usera nie istnieje w bazie!");
        }
        return Ok(user.ToUserDto());
    }

    [HttpPut]
    [Route("change-user-info")]
    public async Task<IActionResult> ChangeUserInfo([FromQuery] string token, [FromBody] UserDto userDto)
    {
        // AJAJAJAJ jak nieladnie. -1000 pointow respektu
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT_KEY"]);
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
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user is null)
        {
            return NotFound("Usera nie istnieje w bazie!");
        }
        userDto.ToUserDetails(user);
        _context.SaveChanges();
        return Content("Data changed", "text/plain");
    }
}

