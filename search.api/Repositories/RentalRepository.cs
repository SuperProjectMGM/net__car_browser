using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using search.api.Data;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Mappers;
using search.api.Models;
using search.api.Services;

namespace search.api.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly IEmailInterface _emailService;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;
    private readonly RabbitMessageService _messageService;

    public RentalRepository(IEmailInterface emailService, IConfiguration configuration, AppDbContext context, RabbitMessageService messageService)
    {
        _emailService = emailService;
        _configuration = configuration;
        _context = context;
        _messageService = messageService;
    }
    
    // TODO: delete rental if link expired or token invalid???

    public async Task<bool> SendConfirmationEmail(string userEmail, string userName, string userId, string scheme, string host,
                                            VehicleRentRequest request)
    {
        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
        {
            return await Task.FromResult(false);
        }
        
        //
        var rentalFirm = await _context.Firms.FirstOrDefaultAsync(x => x.Id == request.RentalFirmId);
        if (rentalFirm is null)
            return await Task.FromResult(false);
        
        // Rental creation
        var rentalModel = await CreateRental(request, userId);
        
        var token = _emailService.GenerateConfirmationRentToken(userEmail, userName, userId, rentalModel.Id, _configuration);
        var confirmationUrl = $"{scheme}://{host}/search.api/Rental/confirm-rental?token={token}";
        
         await _emailService.SendRentalConfirmationEmailAsync(
             userEmail,
             "Rental Confirmation", 
             $"Please confirm your rental of a car. You have 10 minutes to do so.",
             userName, rentalModel.Id, confirmationUrl);
         
         return await Task.FromResult(true);
    }
    

    public (bool, string, string, string, string) ValidateRentalConfirmationToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return (false, null, null, null, null);
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
            var rentId = claimsPrincipal.FindFirst("RentalId")?.Value;
    
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(id)
                                            || string.IsNullOrEmpty(username)
                                            || string.IsNullOrEmpty(rentId))
            {
                return (false, null, null, null, null);
            }

            return (true, email, id, username, rentId);
        }
        catch (SecurityTokenExpiredException)
        {
            return (false, null, null, null, null);
        }
        catch (Exception ex)
        {
            return (false, null, null, null, null);
        }
    }

    public async Task<(Rental?, RentalFirm?)> CompleteRentalAndSend(string email, string id, string username, string rentId)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.Id == rentId);
        if (rental is null)
            return (null, null);
        
        var rentalFirm = await _context.Firms.FirstOrDefaultAsync(x => x.Id == rental.RentalFirmId);
        if (rentalFirm is null)
            return (null, null);

        var userDetails = await _context.UserDetails.FirstOrDefaultAsync(x => x.UserId == id);
        if (userDetails is null)
            return (null, null);
        
        rental.Status = RentalStatus.Confirmed;
        await _context.SaveChangesAsync();
        
        // TODO: send message to data provider api
        var message = CreateRentMessage(rental, userDetails, email, username);
        var success = await _messageService.SendRentalMessage(message);
        if (!success)
            return (null, null);
        return (rental, rentalFirm);
    }

    public async Task<Rental> CreateRental(VehicleRentRequest request, string userId)
    {
        var rentalModel = request.ToRentalFromRequest(userId, request.Description);
        await _context.Rentals.AddAsync(rentalModel);
        await _context.SaveChangesAsync();
        return rentalModel;
    }
    
    public string CreateRentMessage(Rental rental, UserDetails userDetails, string email, string username)
    {
        RentalMessage message = new RentalMessage
        {
            Name = userDetails.Name,
            Surname = userDetails.Surname,
            BirthDate = userDetails.BirthDate,
            DateOfReceiptOfDrivingLicense = userDetails.DateOfReceiptOfDrivingLicense,
            PersonalNumber = userDetails.PersonalNumber,
            LicenceNumber = userDetails.LicenceNumber,
            Address = userDetails.Address,
            PhoneNumber = userDetails.PhoneNumber,
            VinId = rental.VinId,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description
        };
        
        string jsonString = JsonSerializer.Serialize(message);

        return jsonString;
    }
    
    public enum RentalStatus
    {
        Pending = 1,    // Rental request is pending
        Confirmed = 2,  // Rental has been confirmed
        Completed = 3,  // Rental has been completed
    }
    
}