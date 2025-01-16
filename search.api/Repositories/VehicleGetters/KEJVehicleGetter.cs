using System.Collections.Immutable;
using System.Text;
using System.Text.Json;
using Microsoft.Identity.Client;
using NanoidDotNet;
using search.api.DTOs;
using search.api.Helpers;
using search.api.Interfaces;
using search.api.Mappers;
using search.api.Models;

public class KEJVehicleGetter: IVehicleGetter
{
    private readonly string _key = "KEJCO";
    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;
    private readonly IKEJHelper _KEJHelper;
    private readonly string BasePath;

    public KEJVehicleGetter(HttpClient client, IConfiguration configuration, IKEJHelper KEJHelper)
    {
        _client = client;
        _configuration = configuration;
        _KEJHelper = KEJHelper;
        BasePath = _configuration["KEJ_URI"]!;
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

    public async Task<decimal?> GetPriceForCar(VehicleOurDto vehicle, UserDetails user, DateTime start, DateTime end)
    {
        if (vehicle.RentalFirmName == _key)
        {
            var offer = await _KEJHelper.GetOfferForCar(vehicle.Vin, user, start, end);
            return offer!.TotalCost;
        }
        return null;
    }

}