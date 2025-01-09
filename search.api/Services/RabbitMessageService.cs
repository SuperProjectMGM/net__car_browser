
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
    private readonly string _queueProducer = "browserToRental";
    
    private readonly string _queueConsumer = "rentalToBrowser";

    private  IConnection? _connection;

    private IChannel? _channelProducer;

    private IChannel? _channelConsumer;
    
    private readonly IConfiguration _configuration;

    private readonly IServiceProvider _serviceProvider;
    public RabbitMessageService(IConfiguration configuration, IServiceProvider serviceProvider)
    {
        _configuration = configuration;
        _serviceProvider = serviceProvider;
    }
    
    public async Task Register()
    {
        try
        {
            var factory = new ConnectionFactory 
            {
                HostName = _configuration["RABBIT_HOST"]!,
                Port = 5672,
                UserName = _configuration["RABBIT_USERNAME"]!,
                Password = _configuration["RABBIT_PASSWORD"]! 
            };

            _connection = await factory.CreateConnectionAsync();
            _channelProducer = await _connection.CreateChannelAsync();
            _channelConsumer = await _connection.CreateChannelAsync();

            await _channelProducer.QueueDeclareAsync(
                queue: _queueProducer,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            await _channelConsumer.QueueDeclareAsync(
                queue: _queueConsumer,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channelConsumer);
            consumer.ReceivedAsync += async (sender, ea) => await MessageReceived(sender, ea);
            await _channelConsumer.BasicConsumeAsync(_queueConsumer, autoAck: true, consumer: consumer);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during RabbitMQ registration: {ex.Message}");
            Console.Error.WriteLine(ex.StackTrace);   
        }
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
            var messageHandler = scope.ServiceProvider.GetRequiredService<IMessageReceivedWrapper>();
            await messageHandler.ProcessMessage(message);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }

    public async Task RabbitSendMessage(string message)
    {
        var encodedMes = Encoding.UTF8.GetBytes(message);
        var memoryBody = new ReadOnlyMemory<byte>(encodedMes);
        try
        {
            await _channelProducer.BasicPublishAsync(
                exchange: "",
                routingKey: _queueProducer,
                body: memoryBody
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing message: {ex.Message}");
        }
    }
}