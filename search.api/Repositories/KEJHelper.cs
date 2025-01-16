using System.Text;
using System.Text.Json;
using search.api.DTOs;
using search.api.Helpers;
using search.api.Models;

public class KEJHelper : IKEJHelper
{

    private readonly HttpClient _client;
    private readonly IConfiguration _configuration;

    private readonly string PricePath;
    private readonly string AuthPath;

    public KEJHelper(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        _configuration = configuration;
        PricePath = _configuration["KEJ_PRICE_PATH"]!;
        AuthPath = _configuration["KEJ_AUTH_URI"]!;
    }
    public async Task<KEJRentalOfferDto?> GetOfferForCar(string vehicleVin, UserDetails user, DateTime start, DateTime end)
    {
        string token = await GetTokenFromProvider();
        KEJOfferRequestDto dto = new KEJOfferRequestDto 
        {
            CarId = int.Parse(vehicleVin.Split("_")[1]),
            CustomerId = user.Id,
            firstName = user.Name,
            lastName = user.Surname,
            birthday = DateOnly.FromDateTime(user.BirthDate),
            driverLicenseReceiveDate = DateOnly.FromDateTime(user.DrivingLicenseIssueDate),
            RentalName = "MGM Car Rental",
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
        return offer;
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