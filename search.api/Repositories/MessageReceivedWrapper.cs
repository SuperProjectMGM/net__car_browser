using Newtonsoft.Json;
using search.api.Interfaces;
using search.api.Models;
using search.api.Services;

namespace search.api.Repositories;

public class MessageReceivedWrapper : IMessageReceivedWrapper
{
    private IRentalInterface _rentalRepo;
    
    public MessageReceivedWrapper(IRentalInterface rentalRepo)
    {
        _rentalRepo = rentalRepo;
    }
    
    public async Task ProcessMessage(string message)
    {
        var mess = JsonConvert.DeserializeObject<Message>(message);
        if (mess == null)
            throw new Exception("Error during processing message.");
        // switch (mess.MessageType)
        // {
        //     case MessageType.RentalMessageCompletionByEmployee:
        //         await _rentalRepo.RentalCompletion(message);
        //         break;
        //     case MessageType.RentalAcceptedToReturn:
        //         await _rentalRepo.RentalReturnAccepted(mess);
        //         break;
        // }
    }
}