using search.api.Models;

namespace search.api.Interfaces;

public interface IMessageReceivedWrapper
{
    public Task ProcessMessage(string message);
}