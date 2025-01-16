using System.Security.Claims;
using search.api.DTOs;
using search.api.Messages;
using search.api.Models;

namespace search.api.Interfaces;

public interface IRentalInterface
{
    public Task RentalReturnAccepted(EmployeeReturn mess);
    public (string, string, string, string) ValidateClaims(string token);
    public bool ValidateIfTokenHasExpired(string token);
    public Task<Rental?> UserConfirmedRental(int userId, int rentId);
    public Task<bool> ReturnRental(string slug, float longtitude, float latitude, string description);
    public Task<List<Rental>> ReturnUsersRentals(string personalNumber);
    public Task RentalCompletion(Completed mess);
    public Task SendConfirmationEmail(string userEmail,
                                      string userName,
                                      int userId,
                                      string scheme,
                                      string host,
                                      VehicleRentRequestDto requestDto);
}