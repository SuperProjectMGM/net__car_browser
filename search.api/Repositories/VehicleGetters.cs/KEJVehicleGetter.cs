using System.Collections.Immutable;
using Microsoft.Identity.Client;
using search.api.DTOs;
using search.api.Helpers;
using search.api.Interfaces;
using search.api.Mappers;

public class KEJVehicleGetter: IVehicleGetter
{
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly string BasePath;
    public KEJVehicleGetter(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
        BasePath = configuration["KEJ_URI"]!;
    }


    // TODO: Now it's problem with dates like start and end, now I don't know how to manage that. 
    // We only have smth like is car available or not
    public async Task<List<VehicleOurDto>> GetAvailableVehiclesFromRemoteOrigin(DateTime start, DateTime end)
    {
        var response = await _client.GetAsync($"{BasePath}");
        var listOfAllCars = await response.ReadContentAsync<List<VehicleKEJDto>>();
        var available = listOfAllCars.Where(car => car.IsAvailable)
        .Select(car => car.VehicleKEJDtoToOurDto()).Take(10).ToList();
        return available;
    }
}