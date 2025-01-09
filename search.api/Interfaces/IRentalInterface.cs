using System.Security.Claims;
using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces;

public interface IRentalInterface
{
    public Task<bool> SendConfirmationEmail(string userEmail, string userName, int userId, string scheme, string host,
                                            VehicleRentRequestDto requestDto);
    public (string, string, string, string) ValidateClaims(string token);
    public bool ValidateIfTokenHasExpired(string token);
    public Task<Rental?> UserConfirmedRentalSendMessToProvider(int userId, int rentId);
    
    //public Task RentalCompletion(RentalMessage mess);
    public Task<bool> ReturnRental(string slug);
    
    //public Task RentalReturnAccepted(RentalMessage mess);
    public Task<List<Rental>> ReturnUsersRentals(string personalNumber);
}