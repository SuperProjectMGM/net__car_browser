using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NanoidDotNet;
using Newtonsoft.Json;
using search.api.Data;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Mappers;
using search.api.Models;
using search.api.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace search.api.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly IEmailInterface _emailService;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly RabbitMessageService _messageService;
    private readonly AuthDbContext _authDbContext;

    public RentalRepository(IEmailInterface emailService, IConfiguration configuration, AppDbContext context,
        RabbitMessageService messageService,
        AuthDbContext authDbContext)
    {
        _emailService = emailService;
        _configuration = configuration;
        _context = context;
        _messageService = messageService;
        _authDbContext = authDbContext;
    }

    // TODO: delete rental if link expired or token invalid

    public async Task<bool> SendConfirmationEmail(string userEmail, string userName, int userId, string scheme,
        string host,
        VehicleRentRequest request)
    {
        // TODO: We have got do add some logic handling different vehicle providers

        var rentalModel = await CreateRental(request, userId);
        var token = _emailService.GenerateConfirmationRentToken(userEmail, userName, userId, rentalModel.Id,
            _configuration);
        
        var confirmationUrl = $"{scheme}://{host}/search.api/Rental/confirm-rental?token={token}";
        await _emailService.SendRentalConfirmationEmailAsync(userEmail,userName, rentalModel.Slug, confirmationUrl);
        return await Task.FromResult(true);
    }


    public (bool, string, string, string, string) ValidateRentalConfirmationToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return (false, null, null, null, null);
        }
        
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

        var email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        var id = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var username = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        var rentId = claimsPrincipal.FindFirst("RentalId")?.Value;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(id)
                                        || string.IsNullOrEmpty(username)
                                        || string.IsNullOrEmpty(rentId))
        {
            return (false, null, null, null, null);
        }

        return (true, email, id, username, rentId);
    }

    public async Task<Rental?> CompleteRentalAndSend(string email, int id, string username, int rentId)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.Id == rentId);
        if (rental == null)
            return null;
        var userDetails = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userDetails == null)
            return null;

        rental.Status = RentalStatus.Confirmed;
        await _context.SaveChangesAsync();
        var message = CreateRentMessage(rental, userDetails, email);
        var success = await _messageService.SendRentalMessage(message);
        if (!success)
            return null;
        return rental;
    }

    // TODO: we do this now!!!
    public async Task RentalCompletion(RentalMessage mess)
    {
        var dbRental = await _context.Rentals.FirstOrDefaultAsync(x => x.Vin == mess.Vin);
        if (dbRental is null)
            throw new Exception("There is no such rental in DB");

        var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == dbRental.UserId);
        if (user is null)
            throw new Exception("User invalid.");

        dbRental.Status = RentalStatus.Completed;
        await _context.SaveChangesAsync();
        await _emailService.SendRentalCompletionEmailAsync(user.Email!, user.UserName!, dbRental.Slug);
    }

    private async Task<Rental> CreateRental(VehicleRentRequest request, int userId)
    {
        var rentalModel = request.ToRentalFromRequest(userId, request.Description);
        await _context.Rentals.AddAsync(rentalModel);
        await _context.SaveChangesAsync();
        return rentalModel;
    }

    private string CreateRentMessage(Rental rental, UserDetails userDetails, string email)
    {
        RentalMessage message = new RentalMessage
        {
            MessageType = MessageType.RentalMessageConfirmation,
            Slug = rental.Slug,
            Name = userDetails.Name!,
            Surname = userDetails.Surname!,
            BirthDate = userDetails.BirthDate,
            DrivingLicenseIssueDate = userDetails.DrivingLicenseIssueDate!,
            PersonalNumber = userDetails.PersonalNumber!,
            LicenseNumber = userDetails.DrivingLicenseNumber!,
            Email = email,
            City = userDetails.City,
            StreetAndNumber = userDetails.StreetAndNumber,
            PostalCode = userDetails.PostalCode,
            PhoneNumber = userDetails.PhoneNumber!,
            Vin = rental.Vin,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description
        };

        string jsonString = JsonSerializer.Serialize(message);

        return jsonString;
    }

}