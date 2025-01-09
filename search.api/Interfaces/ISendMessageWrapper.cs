using search.api.Models;

namespace search.api.Interfaces;

public interface ISendMessageWrapper
{
    public Task SendMessage(Message message);
}