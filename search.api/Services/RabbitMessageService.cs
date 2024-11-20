
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Services;

public class RabbitMessageService
{
    private readonly string _queueName = "messageBox";

    private  IConnection? _connection;

    private IChannel? _channel;
    
    public async Task Register()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }
    
    public async Task Deregister()
    {
        await this._connection.CloseAsync();
    }

    public async Task<bool> SendRentalMessage(string message)
    {
        var encodedMes = Encoding.UTF8.GetBytes(message);
        var memoryBody = new ReadOnlyMemory<byte>(encodedMes);
        try
        {
            await _channel.BasicPublishAsync(
                exchange: "",
                routingKey: _queueName,
                body: memoryBody
            );
        }
        catch
        {
            return false;
        }

        return true;
    }
}