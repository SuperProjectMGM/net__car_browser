using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Mappers;
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
    //[Authorize(Policy = "DrivingLicenseRequired")]
    [HttpPost("rent-car")]
    public async Task<IActionResult> RentCar([FromBody] VehicleRentRequest request)
    {
        var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var userName = User.FindFirst(ClaimTypes.Name)?.Value;

        if (userEmail == null || userName == null)
            return BadRequest("Invalid user credentials");

        var success = await _rentalRepo.SendConfirmationEmail(userEmail, userName, userId,
            Request.Scheme, Request.Host.ToString(), request);

        if (!success)
        {
            return Unauthorized($"Something went wrong. {userEmail}, {userName}, {userId}");
        }

        return Ok();
    }

    [AllowAnonymous]
    [HttpGet("confirm-rental")]
    public async Task<IActionResult> ConfirmRental([FromQuery] string token)
    {
        if (!_rentalRepo.ValidateIfTokenHasExpired(token))
        {
            return View("TokenExpired");
        }

        (string email, string userId, string userName, string rentId) = _rentalRepo.ValidateClaims(token);
        
        var rental = await _rentalRepo.CompleteRentalAndSend(int.Parse(userId), int.Parse(rentId));

        if (rental is null)
        {
            return NotFound("Something went wrong.");
        }
        
        return View("RentalConfirm", new Tuple<Rental, string, string>(rental, email, userName));
    }

    [HttpPut("return-rental")]
    public async Task<IActionResult> ReturnRental([FromQuery] string slug)
    {
        var success = await _rentalRepo.ReturnRental(slug);
        if (!success)
            return NotFound("Something went wrong. :((");

        return Ok();
    }

    [HttpGet("get-my-rentals")]
    public async Task<IActionResult> MyRentals([FromQuery] string personalNumber)
    {
        var list = await _rentalRepo.ReturnUsersRentals(personalNumber);
        if (list == null)
        {
            return NotFound("Error while finding rentals for user!");
        }
        return Ok(list.Select(element => element.ToRentalDTO()));
    }
}