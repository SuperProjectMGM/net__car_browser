using Newtonsoft.Json;
using search.api.Interfaces;
using search.api.Models;
namespace search.api.Repositories;

public class MessageHandler
{
    private IRentalInterface _rentalRepo;
    
    public MessageHandler(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }
    
    public async Task ProcessMessage(string message)
    {
        var mess = JsonConvert.DeserializeObject<RentalMessage>(message);
        if (mess == null)
            throw new Exception("Error during processing message.");
        switch (mess.MessageType)
        {
            case MessageType.RentalMessageCompletion:
                await _rentalRepo.RentalCompletion(mess);
                break;
            case MessageType.RentalAcceptedToReturn:
                await _rentalRepo.RentalReturnAccepted(mess);
                break;
        }
    }
}