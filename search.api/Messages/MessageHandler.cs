using System.Text.Json;
using search.api.Interfaces;
namespace search.api.Messages;

public class MessageHandler : IMessageHandlerInterface
{
    private IRentalInterface _rentalRepo;
    
    public MessageHandler(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }
    
    public async Task ProcessMessage(string serializedMess)
    {
        var msgWrap = JsonSerializer.Deserialize<MessageWrapper>(serializedMess);
        if (msgWrap is null)
            throw new Exception("Deserialized message corrupted.");
        switch (msgWrap.Type)
        {
            case MessageType.Completed:
                var confirmed = JsonSerializer.Deserialize<Completed>(msgWrap.Message);
                if (confirmed == null)
                    throw new Exception("Completion message corrupted.");
                await _rentalRepo.RentalCompletion(confirmed);
                break;
            case MessageType.EmployeeReturn:
                var returned = JsonSerializer.Deserialize<EmployeeReturn>(msgWrap.Message);
                if (returned == null)
                    throw new Exception("Return acceptance message corrupted.");
                await _rentalRepo.RentalReturnAccepted(returned);
                break;
            default:
                throw new KeyNotFoundException("Unknown message type.");
        }
    }
}