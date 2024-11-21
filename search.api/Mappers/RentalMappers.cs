using NanoidDotNet;
using search.api.DTOs;
using search.api.Models;
using search.api.Repositories;

namespace search.api.Mappers;

public static class RentalMappers
{
    public static Rental ToRentalFromRequest(this VehicleRentRequest vehicleRentRequest, string userId, string desc)
    {
        return new Rental
        {
            Id = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits, 10),
            UserId = userId,
            RentalFirmId = vehicleRentRequest.RentalFirmId,
            VinId = vehicleRentRequest.VehicleVin,
            Start = vehicleRentRequest.Start,
            End = vehicleRentRequest.End,
            Status = RentalRepository.RentalStatus.Pending,
            Description = desc
        };
    }
}