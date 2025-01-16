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
    private readonly string BasePath;
    private readonly string PricePath;
    private readonly string AuthPath;

    public KEJVehicleGetter(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
        BasePath = _configuration["KEJ_URI"]!;
        PricePath = _configuration["KEJ_PRICE_PATH"]!;
        AuthPath = _configuration["KEJ_AUTH_URI"]!;
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
            string token = await GetTokenFromProvider();
            KEJOfferRequestDto dto = new KEJOfferRequestDto 
            {
                CarId = int.Parse(vehicle.Vin.Split("_")[1]),
                CustomerId = user.Id,
                firstName = user.Name,
                lastName = user.Surname,
                birthday = DateOnly.FromDateTime(user.BirthDate),
                driverLicenseReceiveDate = DateOnly.FromDateTime(user.DrivingLicenseIssueDate),
                RentalName = Nanoid.Generate("absddfwe13412", 10),
                PlannedStartDate = start,
                PlannedEndDate = end
            };
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8, "application/json");
            _client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync($"{PricePath}", jsonContent);
            var offer = await response.ReadContentAsync<KEJRentalOfferDto>();
            // RETURN PRICE
            return offer.TotalCost;
        }
        return null;
    }

    public async Task<string> GetTokenFromProvider()
    {
        var logMod = new 
        {
            username = _configuration["KEJ_AUTH_LOGIN"]!,
            password = _configuration["KEJ_AUTH_PASSWORD"]!
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(logMod),
                Encoding.UTF8, "application/json");
            
        var response = await _client.PostAsync($"{AuthPath}", jsonContent);
        ReturnData retData = await response.ReadContentAsync<ReturnData>();
        return retData.Token;
    }

    private class ReturnData
    {
        public string Token { get; set; }
    }
}