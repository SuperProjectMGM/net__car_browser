using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Models;
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
    // Moje pomysly to syf, nie udalo mi sie. Trza poprawic
    //[Authorize(Policy = "DrivingLicenseRequired")]
    [HttpPost("rent-car")]
    public async Task<IActionResult> RentCar([FromBody] VehicleRentRequest request)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value; 
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        var success = await _rentalRepo.SendConfirmationEmail(userEmail, userName, userId,
            Request.Scheme, Request.Host.ToString(), request);
        
        if (!success)
        {
            return Unauthorized($"Something went wrong. {userEmail}, {userName}, {userId}");
        }
        else
        {
            //return View("RentalEmailSent", $"An email has been sent to {userEmail}");
            return Ok();
        }
    }
    
    [AllowAnonymous]
    [HttpGet("confirm-rental")]
    public async Task<IActionResult> ConfirmRental([FromQuery] string token)
    {
        var succeed = _rentalRepo.ValidateRentalConfirmationToken(token);
        
        if (!succeed.Item1)
        {
            return BadRequest("Invalid or expired token.");
        }

        var tuple = await _rentalRepo.CompleteRentalAndSend(succeed.Item2, succeed.Item3, succeed.Item4, succeed.Item5);

        if (tuple.Item1 is null || tuple.Item2 is null)
        {
            return NotFound($"{tuple.Item1}, {tuple.Item2}");
        }

        return View("RentalConfirm", new Tuple<Rental, string, string, RentalFirm>(tuple.Item1, succeed.Item2, succeed.Item4, tuple.Item2));
    }
}