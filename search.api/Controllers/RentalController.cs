using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Repositories;

namespace search.api.Controllers;

[Route("search.api/[controller]")]
[ApiController]
public class RentalController : Controller
{
    private readonly IRentalInterface _rentalRepo;
    
    public RentalController(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }

    [Authorize]
    [HttpPost("rent-car")]
    public async Task<IActionResult> RentCar([FromBody] VehicleRentRequest request)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;
        
        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
        {
            return Unauthorized("User email/name/id not found in token.");
        }
        
        var token = _emailService.GenerateConfirmationToken(userEmail, userName, userId, _configuration);
        
        var confirmationUrl = $"{Request.Scheme}://{Request.Host}/search.api/Rental/confirm-rental?token={token}";
        
         await _emailService.SendRentalConfirmationEmailAsync(userEmail, "Rental Confirmation", 
             $"Please confirm your rental of a car. You have 10 minutes to do so.",
             userName, confirmationUrl, request.StartRent.ToString(), request.EndRent.ToString());
        
        return Ok("Rental request received. Please confirm your rental via the email sent.");
    }
    
    [AllowAnonymous]
    [HttpGet("confirm-rental")]
    public IActionResult ConfirmRental([FromQuery] string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return View("ErrorMessage", "Token is required.");
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
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                ValidAudience = _configuration["JWT:ValidAudience"]
            }, out SecurityToken validatedToken);

            var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
            var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(id) || string.IsNullOrEmpty(username))
            {
                return View("ErrorMessage", "Token is required.");
            }
            
            // here should be logic to inform data provider worker that car has been rented
            // also logic to store in data provider db information about rental
            
            return View("RentalConfirm", "Rental confirmed successfully.");
        }
        catch (SecurityTokenExpiredException)
        {
            return View("ErrorMessage", "Token has expired.");
        }
        catch (Exception ex)
        {
            return View("ErrorMessage", "Error while confirming email/username/id.");
        }
    }
}