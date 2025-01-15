using search.api.DTOs;
using search.api.Helpers;
using search.api.Interfaces;

public class OurVehicleGetter : IVehicleGetter
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly string BasePath;
    public OurVehicleGetter(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
        BasePath = configuration["HttpClientSettingsBaseUrl"]!;
    }

    public async Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end)
    {
        var queryString = $"?start={start:O}&end={end:O}";
        var response = await _client.GetAsync($"{BasePath}{queryString}");
        var listOfVehicledDto = await response.ReadContentAsync<List<VehicleOurDto>>();
        return listOfVehicledDto;
    }
}