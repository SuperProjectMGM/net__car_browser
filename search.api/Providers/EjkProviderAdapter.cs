using search.api.Models;

namespace search.api.Providers;

public class EjkProviderAdapter : IProviderAdapterInterface
{
    private readonly EjkProvider _adaptee;

    public EjkProviderAdapter(HttpClient httpClient, IKEJHelper kejHelper)
    {
        _adaptee = new EjkProvider(httpClient, kejHelper);
    }
    
    public async Task ConfirmRental(Rental rental, UserDetails user)
    {
        await _adaptee.CompleteRental(rental, user);
    }

    public Task ReturnRental(Rental rental)
    {
        throw new NotImplementedException();
    }
}