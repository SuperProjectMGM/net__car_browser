using search.api.Models;

namespace search.api.Interfaces;

public interface ISendMessageInterface
{
    public string CreateRentMessage();

    public bool SendMessageToDataProvider();
}