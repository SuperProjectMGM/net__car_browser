using NanoidDotNet;
using search.api.DTOs;
using search.api.Models;
using search.api.Repositories;

namespace search.api.Mappers;

public static class RentalMappers
{
    public static Rental ToRentalFromRequest(this VehicleRentRequest vehicleRentRequest, int userId, string desc)
    {
        return new Rental
        {
            Slug = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits, 10),
            UserId = userId,
            //RentalFirmId = vehicleRentRequest.RentalFirmId,
            Vin = vehicleRentRequest.VehicleVin,
            Start = vehicleRentRequest.Start,
            End = vehicleRentRequest.End,
            Status = RentalStatus.Pending,
            Description = desc
        };
    }

    public static RentalDTO ToRentalDTO(this Rental rental)
    {
        return new RentalDTO
        {
            Slug = rental.Slug,
            Vin = rental.Vin,
            Start = rental.Start,
            End = rental.End,
            Description = rental.Description,
            Status = rental.Status.ToString()
        };
    }
}