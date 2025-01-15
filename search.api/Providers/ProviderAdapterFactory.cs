using search.api.Services;

namespace search.api.Providers;

public static class ProviderAdapterFactory
{
    public static IProviderAdapterInterface CreateProvider(string identifier, RabbitMessageService service)
    {
        switch (identifier)
        {
            case "MGMCO":
                return new MgmProviderAdapter(service);
            default:
                throw new KeyNotFoundException("Unknown provider.");
        }
    }
}