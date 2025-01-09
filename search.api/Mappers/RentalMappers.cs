using NanoidDotNet;
using search.api.DTOs;
using search.api.Models;
using search.api.Repositories;

namespace search.api.Mappers;

public static class RentalMappers
{
    public static Rental ToRentalFromRequest(this VehicleRentRequestDto vehicleRentRequestDto, int userId, string desc)
    {
        return new Rental
        {
            Slug = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits, 10),
            UserId = userId,
            CarProviderIdentifier = vehicleRentRequestDto.CarProviderIdentifier,
            Vin = vehicleRentRequestDto.VehicleVin,
            Start = vehicleRentRequestDto.Start,
            End = vehicleRentRequestDto.End,
            Status = RentalStatus.Pending,
            Description = desc
        };
    }

    public static RentalDto ToRentalDTO(this Rental rental)
    {
        return new RentalDto
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