using System.Diagnostics;
using search.api.Models;

namespace search.api.Messages;

public interface IMessageHandlerInterface
{
    public Task ProcessMessage(string serializedMess);
}