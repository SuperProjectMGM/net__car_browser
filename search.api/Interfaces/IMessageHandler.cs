using search.api.Models;

namespace search.api.Interfaces;

public interface IMessageHandler
{
    public Task ProcessMessage(string message);
}