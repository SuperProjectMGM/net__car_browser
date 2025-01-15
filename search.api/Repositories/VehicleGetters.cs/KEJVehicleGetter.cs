using search.api.DTOs;
using search.api.Interfaces;

public class KEJVehicleGetter: IVehicleGetter
{
    private readonly HttpClient _client;
    // TODO: Change when real one will be given
    private readonly string BasePath = "example/endpoint";
    public KEJVehicleGetter(HttpClient client)
    {
        _client = client;
    }

    public Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end)
    {
        throw new NotImplementedException();
    }
}