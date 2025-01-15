using search.api.DTOs;

namespace search.api.Interfaces;

public interface IVehicleGetter
{
    public Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end);
}