using System.Diagnostics;
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

    public bool ValidateIfTokenHasExpired(string token)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtTokenHandler.ReadJwtToken(token);
        if (jwtToken.ValidTo < DateTime.UtcNow)
            return false;
        return true;
    }
    
    public (string, string, string, string) ValidateClaims(string token)
    {
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
            return (null, null, null, null);
        }

        return (email, id, username, rentId);
    }

    
    public async Task<Rental?> CompleteRentalAndSend(int userId, int rentId)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync(x => x.Id == rentId);
        if (rental == null)
            return null;
        var userDetails = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (userDetails == null)
            return null;

        rental.Status = RentalStatus.ConfirmedByUser;
        await _context.SaveChangesAsync();
        
        
        var message = Message.MessageFactoryRentalConfirmedByUser(rental, userDetails);
        
        string jsonString = JsonSerializer.Serialize(message);
        var success = await _messageService.SendMessage(jsonString);
        if (!success)
            return null;
        return rental;
    }

    public async Task RentalCompletion(RentalMessage mess)
    {
        var dbRental = await _context.Rentals.FirstOrDefaultAsync(x => x.Slug == mess.Slug);
        if (dbRental is null)
            throw new Exception("There is no such rental in DB");

        var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == dbRental.UserId);
        if (user is null)
            throw new Exception("User invalid.");

        dbRental.Status = RentalStatus.CompletedByEmployee;
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

    public async Task<bool> ReturnRental(string slug)
    {
        var rental = await _context.Rentals.FirstOrDefaultAsync((rental) => rental.Slug == slug);
        if (rental == null)
            return false;
        var userDetails = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == rental!.UserId);
        if (userDetails == null)
            return false;

        rental.Status = RentalStatus.WaitingForReturnAcceptance;
        await _context.SaveChangesAsync();
        var mess = CreateRentMessage(rental, userDetails);
        mess.MessageType = MessageType.RentalToReturn;
        var jsonStr = JsonSerializer.Serialize(mess);
        var success = await _messageService.SendMessage(jsonStr);
        return success;
    }

    public async Task RentalReturnAccepted(RentalMessage mess)
    {
        var dbRental = await _context.Rentals.FirstOrDefaultAsync(x => x.Slug == mess.Slug);
        if (dbRental is null)
            throw new Exception("There is no such rental in DB");

        var user = await _authDbContext.Users.FirstOrDefaultAsync(x => x.Id == dbRental.UserId);
        if (user is null)
            throw new Exception("User invalid.");
        
        dbRental.Status = RentalStatus.Returned;
        
        await _context.SaveChangesAsync();
    }

    // private RentMessage CreateRentMessage(Rental rental, UserDetails userDetails)
    // {
    //
    //
    //     return message;
    // }

    public async Task<List<Rental>> ReturnUsersRentals(string personalNumber)
    {
        var user = await _authDbContext.Users.FirstOrDefaultAsync((user) => user.PersonalNumber == personalNumber);
        int id = user!.Id;
        return await _context.Rentals.Where((rent) => rent.UserId == id).ToListAsync();
    }
}