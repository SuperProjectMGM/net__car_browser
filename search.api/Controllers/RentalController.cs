using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NanoidDotNet;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Services;


namespace search.api.Controllers;

[Route("search.api/[controller]")]
[ApiController]
public class RentalController : ControllerBase
{
    private readonly IEmailInterface _emailService;
    private readonly IConfiguration _configuration;

    public RentalController(IConfiguration configuration, IEmailInterface emailService)
    {
        _emailService = emailService;
        _configuration = configuration;
    }

    [Authorize]
    [HttpPost("rent-car")]
    public async Task<IActionResult> RentCar([FromBody] VehicleRentRequest request)
    {
        var userEmail = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User email not found in token.");
        }
        
        // NOT WORKING 
        var token = GenerateConfirmationToken(userEmail, request.CarId);
        var confirmationUrl = $"http://localhost:5178/confirm-rental?token={token}";

        await _emailService.SendRentalConfirmationEmailAsync("Rental Confirmation", userEmail,
            $"Please confirm your rental of a car. You have 10 minutes to do so.\n" +
                    $"Start date: {request.StartRent}\n" +
                    $"End date: {request.EndRent}\n" +
                    $"Click in this link to confirm:\n {confirmationUrl}");
        
        return Ok("Rental request received. Please confirm your rental via the email sent.");
    }
    
    [AllowAnonymous]
    [HttpGet("confirm-rental")]
    public IActionResult ConfirmRental([FromQuery] string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Token is required.");
        }
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidIssuer = "your-issuer",
                ValidAudience = "your-api"
            }, out SecurityToken validatedToken);

            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Invalid token payload.");
            }
            
            // here should be logic to inform data provider worker that car has been rented
            // also logic to store in data provider db information about rental
            
            return Ok("Rental confirmed successfully!");
        }
        catch (SecurityTokenExpiredException)
        {
            return Unauthorized("Token has expired.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error confirming rental: {ex.Message}");
        }
    }
    
    // dunno if these functions should be here, open to suggestions 
    private string GenerateConfirmationToken(string email, int carId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim("CarId", carId.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-api",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}