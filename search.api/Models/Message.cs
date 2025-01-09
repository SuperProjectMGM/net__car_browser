namespace search.api.Models;

public class Message
{
    public MessageType MessageType;

    /// <summary>
    /// This method decides what message to create, based on provider of a car (stored in rental object).
    /// Message type is set inside this function.
    /// </summary>
    /// <param name="rental"></param>
    /// <param name="userDetails"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static Message MessageFactoryRentalConfirmedByUser(Rental rental, UserDetails userDetails)
    {
        // For now hardcoded data provider identifiers.
        if (rental.CarProviderIdentifier == "MGMCO")
        {
            RentMessage message = new RentMessage
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
            message.MessageType = MessageType.OurRentalUserConfirmedRental;
            return message;
        }
        else if (rental.CarProviderIdentifier == "ERNESTOS")
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new Exception("Unknown message to send.");
        }
    }
}
public enum MessageType
{
    OurRentalUserConfirmedRental = 0,
    OurRentalEmployeeAcceptedRental = 1,
    RentalToReturn = 2,
    RentalAcceptedToReturn= 3
}
