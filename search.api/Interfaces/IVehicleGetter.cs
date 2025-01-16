using search.api.DTOs;
using search.api.Models;

namespace search.api.Interfaces;

public interface IVehicleGetter
{
    public Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end);
    public Task<decimal?> GetPriceForCar(VehicleOurDto vehicle, UserDetails user, DateTime start, DateTime end);
    public Task<string> GetTokenFromProvider();
}