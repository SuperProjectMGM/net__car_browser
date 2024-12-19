using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces;

public interface IRentalInterface
{
    public Task<bool> SendConfirmationEmail(string userEmail, string userName, int userId, string scheme, string host,
                                            VehicleRentRequest request);

    public (bool, string, string, string, string) ValidateRentalConfirmationToken(string token);

    public Task<Rental?> CompleteRentalAndSend(int userId, int rentId);

    //public Task<Rental> CreateRental(VehicleRentRequest request, string userId);

    public Task RentalCompletion(RentalMessage mess);

    public Task<bool> ReturnRental(int userId, int rentalId);
}