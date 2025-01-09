using System.Text.Json.Serialization;
namespace search.api.Models;

[JsonDerivedType(typeof(Message_OurAPI_RentConfirmed), typeDiscriminator: "Mess_OurApi_RentConfirmed")]
public class Message
{
    /// <summary>
    /// Convention of naming files that contain messages models is:
    /// [ReceivedFromApi]_Message_[ApiToSend]_[ActionHappened].cs, 
    /// first and last brackets [] may be empty depending on type of message.
    /// </summary>
    
    public MessageType MessageType;

    /// <summary>
    /// This method decides what message to create, based on provider of a car (stored in rental object).
    /// Message type is set inside this function.
    /// </summary>
    public static Message MessageFactoryRentalConfirmedByUser(Rental rental, UserDetails userDetails)
    {
        // For now hardcoded data provider identifiers.
        // Identifiers can be stored in secrets or db i dunno.
        if (rental.CarProviderIdentifier == "MGMCO")
        {
            Message_OurAPI_RentConfirmed messageOurApi = new Message_OurAPI_RentConfirmed
            {
                Slug = rental.Slug,
                Name = userDetails.Name!,
                Surname = userDetails.Surname!,
                BirthDate = userDetails.BirthDate,
                DrivingLicenseIssueDate = userDetails.DrivingLicenseIssueDate!,
                PersonalNumber = userDetails.PersonalNumber!,
                LicenseNumber = userDetails.DrivingLicenseNumber!,
                Email = userDetails.Email!,
                City = userDetails.City,
                StreetAndNumber = userDetails.StreetAndNumber,
                PostalCode = userDetails.PostalCode,
                PhoneNumber = userDetails.PhoneNumber!,
                Vin = rental.Vin,
                Start = rental.Start,
                End = rental.End,
                Status = rental.Status,
                Description = rental.Description
            };
            messageOurApi.MessageType = MessageType.RentalConfirmedByUser_OurAPI;
            return messageOurApi;
        }
        else if (rental.CarProviderIdentifier == "ERNESTOS")
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new Exception("Unknown message to create.");
        }
    }
}
public enum MessageType
{
    RentalConfirmedByUser_OurAPI = 0,
    RantalCompletedByEmployee_OurAPI = 1,
    RentalToReturn = 2,
    RentalAcceptedToReturn= 3
}
