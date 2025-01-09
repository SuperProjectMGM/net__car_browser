using System.Text.Json;
using search.api.Interfaces;
using search.api.Models;
using search.api.Services;

namespace search.api.Repositories;

public class SendMessageWrapper : ISendMessageWrapper
{
    private readonly RabbitMessageService _rabbitService;

    public SendMessageWrapper(RabbitMessageService rabbitService)
    {
        _rabbitService = rabbitService;
    }
    
    public async Task SendMessage(Message message)
    {
        var jsonStr = SerializeMessage(message);
        if (message.MessageType == MessageType.RentalConfirmedByUser_OurAPI)
        {
            await _rabbitService.RabbitSendMessage(jsonStr);
        }
    }

    private string SerializeMessage(Message message)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        return JsonSerializer.Serialize(message, options);
    }
}