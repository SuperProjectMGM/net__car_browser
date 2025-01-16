using search.api.Models;

namespace search.api.Providers;

public class EjkProvider
{
    private readonly HttpClient _httpClient;
    
    private string token = string.Empty;

    public EjkProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task CompleteRental(Rental rental)
    {
        rental.Status = RentalStatus.CompletedByEmployee;
        throw new NotImplementedException();
    }

    public async Task ReturnRental(Rental rental)
    {
        rental.Status = RentalStatus.WaitingForReturnAcceptance;
        throw new NotImplementedException();
    }
}