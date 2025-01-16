using search.api.DTOs;
using search.api.Helpers;
using search.api.Interfaces;
using search.api.Models;

public class OurVehicleGetter : IVehicleGetter
{
    private readonly string _key = "MGMCO";
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly string BasePath;
    public OurVehicleGetter(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
        BasePath = _configuration["HttpClientSettingsBaseUrl"]!;
    }

    public async Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end)
    {
        var queryString = $"?start={start:O}&end={end:O}";
        var response = await _client.GetAsync($"{BasePath}{queryString}");
        var listOfVehicledDto = await response.ReadContentAsync<List<VehicleOurDto>>();
        return listOfVehicledDto;
    }

    public async Task<decimal?> GetPriceForCar(VehicleOurDto vehicle, UserDetails user, DateTime start, DateTime end)
    {
        if (vehicle.RentalFirmName == _key)
        {
            decimal baseCostPerDay = vehicle.Price;
            int totalDays = (int)Math.Ceiling((end - start).TotalDays);
            int numberOfYearsLicense = (int)Math.Floor((end - start).TotalDays);
            decimal resCostPerDay = 1.2m * baseCostPerDay - baseCostPerDay * 0.05m * (numberOfYearsLicense / 10m);
            return resCostPerDay;
        }
        return null;
    }
}
