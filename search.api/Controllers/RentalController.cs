using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms.Mapping;
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
        var succeed = _rentalRepo.ValidateRentalConfirmationToken(token);

        int item3 = int.Parse(succeed.Item3);
        int item5 = int.Parse(succeed.Item5);

        if (!succeed.Item1)
        {
            return BadRequest("Invalid or expired token.");
        }

        var rental = await _rentalRepo.CompleteRentalAndSend(item3, item5);

        if (rental is null)
        {
            return NotFound("Something went wrong :(");
        }

        return View("RentalConfirm", new Tuple<Rental, string, string>(rental, succeed.Item2, succeed.Item4));
    }

    [HttpPut("return-rental")]
    public async Task<IActionResult> ReturnRental([FromQuery] int userId, int rentId)
    {
        var success = await _rentalRepo.ReturnRental(userId, rentId);
        if (!success)
            return NotFound("Something went wrong. :((");

        return Ok();
    }

    // TODO: zrobić to
    [HttpGet("get-my-rentals")]
    public async Task<IActionResult> MyRentals([FromQuery] int userId)
    {
        
    }
}