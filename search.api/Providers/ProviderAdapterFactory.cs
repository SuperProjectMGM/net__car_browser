using search.api.Services;

namespace search.api.Providers;

public static class ProviderAdapterFactory
{
    public static IProviderAdapterInterface CreateProvider(string identifier,
                                                           RabbitMessageService service,
                                                           HttpClient httpClient,
                                                           IKEJHelper kejHelper)
    {
        switch (identifier)
        {
            case "MGMCO":
                return new MgmProviderAdapter(service);
            case "KEJCO":
                return new EjkProviderAdapter(httpClient, kejHelper);
            default:
                throw new KeyNotFoundException("Unknown provider.");
        }
    }
}