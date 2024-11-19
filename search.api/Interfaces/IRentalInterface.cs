using search.api.DTOs;

namespace search.api.Interfaces;

public interface IRentalInterface
{
    public Task<bool> SendConfirmationEmail(string userEmail, string userName, string userId, string scheme, string host,
                                            VehicleRentRequest request);
}