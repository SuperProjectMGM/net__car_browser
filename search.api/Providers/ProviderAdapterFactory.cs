using search.api.Services;

namespace search.api.Providers;

public static class ProviderAdapterFactory
{
    public static IProviderAdapterInterface CreateProvider(string identifier, RabbitMessageService service, HttpClient httpClient)
    {
        switch (identifier)
        {
            case "MGMCO":
                return new MgmProviderAdapter(service);
            case "EJKCO":
                return new EjkProviderAdapter(httpClient);
            default:
                throw new KeyNotFoundException("Unknown provider.");
        }
    }
}