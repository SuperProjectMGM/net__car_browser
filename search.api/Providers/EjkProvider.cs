using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using search.api.DTOs;
using search.api.Helpers;
using search.api.Models;

namespace search.api.Providers;

public class EjkProvider
{
    private readonly HttpClient _httpClient;

    private readonly IKEJHelper _kejHelper;
    public EjkProvider(HttpClient httpClient, IKEJHelper kejHelper)
    {
        _httpClient = httpClient;
        _kejHelper = kejHelper;
    }
    
    public async Task CompleteRental(Rental rental, UserDetails user)
    {
        rental.Status = RentalStatus.CompletedByEmployee;
        var token = await _kejHelper.GetTokenFromProvider();
        var offer = await _kejHelper.GetOfferForCar(rental.Vin, user, rental.Start, rental.End);
        if (offer == null)
            throw new Exception("Ejk provider did not provide an offer.");
        var dto = new KEJRental();
        dto.CustomerId = user.Id;
        dto.OfferId = offer.Id;
        dto.RentalName = "MGM Car Rental";
        dto.PlannedStartDate = rental.Start;
        dto.PlannedEndDate = rental.End;
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        var jsonContent = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        Console.WriteLine(JsonSerializer.Serialize(dto));
        _httpClient.DefaultRequestHeaders.Authorization = 
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync("https://car-rental-api-chezbchwebfggwcd.canadacentral-01.azurewebsites.net/api/customer/rentals", jsonContent);
            KEJRentalReturn returnRentInfo = await response.ReadContentAsync<KEJRentalReturn>();
            rental.Slug += $"_{returnRentInfo.Id}";
    }

    public async Task ReturnRental(Rental rental)
    {
        var token = await _kejHelper.GetTokenFromProvider();
        KEJReturnRequest returnRequest = new KEJReturnRequest
        {
            RentalId = int.Parse(rental.Slug.Split("_")[1])
        };
        try 
        {
            
            //var request = new HttpRequestMessage(HttpMethod.Post,
            //    "http://car-rental-api-chezbchwebfggwcd.canadacentral-01.azurewebsites.net/api/customer/rentals/return"); 
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //request.Content = JsonContent.Create(returnRequest);
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(returnRequest),
                Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = 
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.PostAsync("https://car-rental-api-chezbchwebfggwcd.canadacentral-01.azurewebsites.net/api/customer/rentals/return", jsonContent);
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during ejk rental return: {ex.Message}");
            throw;
        }
    }

    private class KEJReturnRequest
    {
        public int RentalId { get; set; }
    }
}