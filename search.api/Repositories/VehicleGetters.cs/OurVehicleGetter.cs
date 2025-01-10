using search.api.DTOs;
using search.api.Helpers;
using search.api.Interfaces;

public class OurVehicleGetter : IVehicleGetter
{
    private readonly HttpClient _client;
    private readonly string BasePath = "api/Vehicle/available";
    public OurVehicleGetter(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end)
    {
        var queryString = $"?start={start:O}&end={end:O}";
        var response = await _client.GetAsync($"/{BasePath}{queryString}");
        var listOfVehicledDto = await response.ReadContentAsync<List<VehicleOurDto>>();
        return listOfVehicledDto;
    }
}