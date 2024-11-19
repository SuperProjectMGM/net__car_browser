using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces;

public interface IRentalInterface
{
    public Task<bool> SendConfirmationEmail(string userEmail, string userName, string userId, string scheme, string host,
                                            VehicleRentRequest request);

    public (bool, string, string, string, string) ValidateRentalConfirmationToken(string token);

    public Task<(Rental?, RentalFirm?)> CompleteRentalAndSend(string email, string id, string username, string rentId);

    public Task<Rental> CreateRental(VehicleRentRequest request, string userId);
}