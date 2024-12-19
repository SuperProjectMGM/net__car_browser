
using System.Text;
using System.Text.Json;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Services;

public class RabbitMessageService
{
    private readonly string _queueName = "messageBox";
    
    private readonly string _queueName2 = "messageBox2";

    private  IConnection? _connection;

    private IChannel? _channel;

    private IChannel? _channel2;
    
    private readonly IServiceProvider _serviceProvider;

    public RabbitMessageService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task Register()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();
        _channel2 = await _connection.CreateChannelAsync();

        await _channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        await _channel2.QueueDeclareAsync(
            queue: _queueName2,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(_channel2);
        consumer.ReceivedAsync += async (sender, ea) => await MessageReceived(sender, ea);
        await _channel2.BasicConsumeAsync(_queueName2, autoAck: true, consumer: consumer);
    }
    
    public async Task Deregister()
    {
        await this._connection.CloseAsync();
    }
    
    private async Task MessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        try
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            using var scope = _serviceProvider.CreateScope();
            var messageHandler = scope.ServiceProvider.GetRequiredService<IMessageHandler>();
            await messageHandler.ProcessMessage(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
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