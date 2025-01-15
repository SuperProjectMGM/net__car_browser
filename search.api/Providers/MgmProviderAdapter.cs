using Microsoft.AspNetCore.Components;
using search.api.Models;
using search.api.Services;

namespace search.api.Providers;

public class MgmProviderAdapter : IProviderAdapterInterface
{
    public MgmProviderAdapter(RabbitMessageService service)
    {
        _adaptee = new MgmProvider(service);
    }

    private readonly MgmProvider _adaptee;
    
    public async Task ConfirmRental(Rental rental, UserDetails user)
    {
        await _adaptee.ConfirmRental(rental, user);
    }

    public async Task ReturnRental(Rental rental)
    {
        await _adaptee.ReturnRental(rental);
    }

}