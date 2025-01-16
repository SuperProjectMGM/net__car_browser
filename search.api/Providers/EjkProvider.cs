using System.Net.Http.Headers;
using search.api.DTOs;
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
        var dto = new KEJRental
        {
            CustomerId = user.Id,
            OfferId = offer.Id,
            RentalName = "MGM Car Rental",
            PlannedStartDate = rental.Start,
            PlannedEndDate = rental.End
        };
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                "http://car-rental-api-chezbchwebfggwcd.canadacentral-01.azurewebsites.net/api/customer/rentals/offers");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Content = JsonContent.Create(dto);
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {response.StatusCode}, Details: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during ejk rental completion: {ex.Message}");
            throw;
        }
    }

    public async Task ReturnRental(Rental rental)
    {
        rental.Status = RentalStatus.WaitingForReturnAcceptance;
        throw new NotImplementedException();
    }
}