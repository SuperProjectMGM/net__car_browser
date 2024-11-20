using search.api.Models;

namespace search.api.Interfaces;

public interface ISendMessageInterface
{
    public string CreateRentMessage(Rental rental, UserDetails userDetails, string email, string username);

    public Task<bool> SendMessageToDataProvider(string message);
}