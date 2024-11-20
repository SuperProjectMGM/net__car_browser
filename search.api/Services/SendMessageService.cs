
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Services;

public class SendMessageService : ISendMessageInterface
{
    private readonly string _queueName = "messageBox";
    
    public string CreateRentMessage(Rental rental, UserDetails userDetails, string email, string username)
    {
        RentalMessage message = new RentalMessage
        {
            Name = userDetails.Name,
            Surname = userDetails.Surname,
            BirthDate = userDetails.BirthDate,
            DateOfReceiptOfDrivingLicense = userDetails.DateOfReceiptOfDrivingLicense,
            PersonalNumber = userDetails.PersonalNumber,
            LicenceNumber = userDetails.LicenceNumber,
            Address = userDetails.Address,
            PhoneNumber = userDetails.PhoneNumber,
            VinId = rental.VinId,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description
        };
        
        string jsonString = JsonSerializer.Serialize(message);

        return jsonString;
    }

    public async Task<bool> SendMessageToDataProvider(string message)
    {
        // temporary HostName
        var factory = new ConnectionFactory { HostName = "localhost" };

        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var encodedMes = Encoding.UTF8.GetBytes(message);
        var memoryBody = new ReadOnlyMemory<byte>(encodedMes);

        try
        {
            await channel.BasicPublishAsync(
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