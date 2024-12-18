using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces;

public interface IRentalInterface
{
    public Task<bool> SendConfirmationEmail(string userEmail, string userName, int userId, string scheme, string host,
                                            VehicleRentRequest request);

    public (bool, string, string, string, string) ValidateRentalConfirmationToken(string token);

    public Task<Rental?> CompleteRentalAndSend(string email, int id, string username, int rentId);

    //public Task<Rental> CreateRental(VehicleRentRequest request, string userId);

    public Task RentalCompletion(string message);
}