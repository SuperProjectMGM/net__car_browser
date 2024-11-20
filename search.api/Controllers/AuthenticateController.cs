using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NanoidDotNet;
using search.api.Models;



namespace search.api.Controllers;

[Route("search.api/[controller]")]
[ApiController]
public class AuthenticateController: ControllerBase
{
    private readonly UserManager<UserDetails> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<UserDetails> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        // Now check if exists the user with same email. I think we can't have two users with same email
        var userExists = await _userManager.FindByEmailAsync(model.Email!);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "User with this email already exists!");
        }

        UserDetails user = new () 
        {
            Email = model.Email,
            UserName = model.UserName,
            Name = model.FirstName,
            Surname = model.SecondName,
            BirthDate = model.DateOfBirth,
            AddressStreet = model.AddressStreet,
            PostalCode = model.PostalCode,
            City = model.City,
            DrivingLicenseNumber = model.DrivingLicenseNumber,
            DrivingLicenseIssueDate = model.DrivingLicenseIssueDate,
            DrivingLicenseExpirationDate = model.DrivingLicenseExpirationDate,
            IdPersonalNumber = model.IdCardNumber,
            IdCardIssuedBy = model.IdCardIssuedBy,
            IdCardIssueDate = model.IdCardIssueDate,
            IdCardExpirationDate = model.IdCardExpirationDate,
            // It should be changed whenever any cridential was changed???
            SecurityStamp = Guid.NewGuid().ToString()

        };
        var result = await _userManager.CreateAsync(user, model.Password!);
        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            return StatusCode(StatusCodes.Status500InternalServerError, $"User creation failed: {errorMessages}");
        }
        return Ok();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password!))
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var token = GetToken(authClaims);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));
        return token;
    }    
}