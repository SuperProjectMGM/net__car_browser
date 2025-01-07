using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using search.api.Models;



namespace search.api.Controllers;

[Route("search.api/[controller]")]
[ApiController]
public class AuthenticateController: ControllerBase
{
    private readonly UserManager<UserDetails> _userManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<UserDetails> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
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
            // TODO: mapper tu musi być
            Email = model.Email,
            UserName = model.UserName,
            Name = model.Name,
            Surname = model.Surname,
            PhoneNumber = model.PhoneNumber,
            BirthDate = model.DateOfBirth,
            StreetAndNumber = model.StreetAndNumber,
            PostalCode = model.PostalCode,
            City = model.City,
            DrivingLicenseNumber = model.DrivingLicenseNumber,
            DrivingLicenseIssueDate = model.DrivingLicenseIssueDate,
            DrivingLicenseExpirationDate = model.DrivingLicenseExpirationDate,
            PersonalNumber = model.PersonalNumber,
            IdCardIssuedBy = model.IdCardIssuedBy,
            IdCardIssueDate = model.IdCardIssueDate,
            IdCardExpirationDate = model.IdCardExpirationDate,
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
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

    // I don't understand why get not post?
    [HttpGet]
    [Route("login/google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = _configuration["GOOGLE_AUTH_REDIRECT_URI"]
        };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("login/google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!authenticateResult.Succeeded)
            return BadRequest();

        var claims = authenticateResult.Principal?.Claims;
        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        var user = await _userManager.FindByEmailAsync(email!);
        if (user == null)
        {
            return Unauthorized();
        }
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var token = GetToken(authClaims);
        return Ok(new {token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo} );
    }
    
    private JwtSecurityToken GetToken(List<Claim> authClaims)
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
}