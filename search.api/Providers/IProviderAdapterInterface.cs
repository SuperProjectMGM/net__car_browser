using search.api.Models;

namespace search.api.Providers;

public interface IProviderAdapterInterface
{
    public Task ConfirmRental(Rental rental, UserDetails user);

    public Task ReturnRental(Rental rental);
}