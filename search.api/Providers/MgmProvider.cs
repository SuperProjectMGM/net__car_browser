using System.Text.Json;
using search.api.Messages;
using search.api.Models;
using search.api.Services;

namespace search.api.Providers;

public class MgmProvider
{
    private readonly RabbitMessageService _service;
    public MgmProvider(RabbitMessageService service)
    {
        _service = service;
    }
    
    public async Task ConfirmRental(Rental rental, UserDetails user)
    {
        rental.Status = RentalStatus.ConfirmedByUser;

        var message = new Confirmed
        {
            BrowserProviderIdentifier = "MGMCO",
            Slug = rental.Slug,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email!,
            BirthDate = user.BirthDate,
            DrivingLicenseIssueDate = user.DrivingLicenseIssueDate,
            PersonalNumber = user.PersonalNumber,
            LicenseNumber = user.DrivingLicenseNumber,
            City = user.City,
            StreetAndNumber = user.StreetAndNumber,
            PostalCode = user.PostalCode,
            PhoneNumber = user.PhoneNumber!,
            Vin = rental.Vin,
            Start = rental.Start,
            End = rental.End,
            Status = rental.Status,
            Description = rental.Description
        };
        var jsonString = JsonSerializer.Serialize(message);
        var messageWrap = new MessageWrapper
        {
            Message = jsonString,
            Type = MessageType.Confirmed
        };
        var jsonWrap = JsonSerializer.Serialize(messageWrap);
        await _service.SendMessage(jsonWrap);
    }

    public async Task ReturnRental(Rental rental)
    {
        rental.Status = RentalStatus.WaitingForReturnAcceptance;
        var message = new UserReturn
        {
            Slug = rental.Slug
        };
        var jsonString = JsonSerializer.Serialize(message);
        var messageWrap = new MessageWrapper
        {
            Message = jsonString,
            Type = MessageType.UserReturn
        };
        var jsonWrap = JsonSerializer.Serialize(messageWrap);
        await _service.SendMessage(jsonWrap);
    }
}