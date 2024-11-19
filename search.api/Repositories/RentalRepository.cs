using search.api.Data;
using search.api.DTOs;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Repositories;

public class RentalRepository : IRentalInterface
{
    private readonly IEmailInterface _emailService;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _context;

    public RentalRepository(IEmailInterface emailService, IConfiguration configuration, AppDbContext context)
    {
        _emailService = emailService;
        _configuration = configuration;
        _context = context;
    }

    public async Task<bool> SendConfirmationEmail(string userEmail, string userName, string userId, string scheme, string host,
                                            VehicleRentRequest request)
    {
        if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(userName))
        {
            return await Task.FromResult(false);
        }
        
        
        
        var token = _emailService.GenerateConfirmationRentToken(userEmail, userName, userId, _configuration);
        
        var confirmationUrl = $"{scheme}://{host}/search.api/Rental/confirm-rental?token={token}";
        
         await _emailService.SendRentalConfirmationEmailAsync(userEmail, "Rental Confirmation", 
             $"Please confirm your rental of a car. You have 10 minutes to do so.",
             userName, confirmationUrl, request.StartRent.ToString(), request.EndRent.ToString());

         return await Task.FromResult(true);
    }
    
    
}