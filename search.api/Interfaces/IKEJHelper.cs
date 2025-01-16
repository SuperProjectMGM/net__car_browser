using search.api.DTOs;
using search.api.Models;

public interface IKEJHelper
{
    public Task<KEJRentalOfferDto?> GetOfferForCar(VehicleOurDto vehicle, UserDetails user, DateTime start, DateTime end);
    public Task<string> GetTokenFromProvider();
}