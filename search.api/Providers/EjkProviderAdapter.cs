using search.api.Models;

namespace search.api.Providers;

public class EjkProviderAdapter : IProviderAdapterInterface
{
    private readonly EjkProvider _adaptee;

    public EjkProviderAdapter(HttpClient httpClient)
    {
        _adaptee = new EjkProvider(httpClient);
    }
    
    public async Task ConfirmRental(Rental rental, UserDetails user)
    {
        await _adaptee.CompleteRental(rental);
    }

    public Task ReturnRental(Rental rental)
    {
        throw new NotImplementedException();
    }
}