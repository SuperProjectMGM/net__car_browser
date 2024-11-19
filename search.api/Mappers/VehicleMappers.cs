using search.api.DTOs;
using search.api.Models;
using System;

namespace search.api.Mappers
{
    public static class VehicleMappers
    {
        public static Vehicle VehicleOurDtoToVehicle(this VehicleOurDto dto)
        {
            return new Vehicle
            {
                CarId = dto.CarId,
                Brand = dto.Brand,
                Model = dto.Model,
                SerialNo = dto.SerialNo,
                VinId = dto.VinId,
                RegistryNo = dto.RegistryNo,
                YearOfProduction = dto.YearOfProduction,  // Zmienna jest już typu int
                RentalFrom = dto.RentalFrom,  // Zmienna jest już typu DateTime
                RentalTo = dto.RentalTo,  // Zmienna jest już typu DateTime
                Prize = dto.Prize,  // Zmienna jest już typu decimal
                DriveType = dto.DriveType,
                Transmission = dto.Transmission,
                Description = dto.Description,
                Type = dto.Type,
                Rate = dto.Rate,  // Zmienna jest już typu decimal
                Localization = dto.Localization,
                RentalFirm = new RentalFirm() // TODO: Uzupełnij dane RentalFirm, jeśli są dostępne
            };
        }

        public static VehicleOurDto VehicleToVehicleOurDto(this Vehicle veh)
        {
            return new VehicleOurDto
            {
                CarId = veh.CarId,
                Brand = veh.Brand,
                Model = veh.Model,
                SerialNo = veh.SerialNo,
                VinId = veh.VinId,
                RegistryNo = veh.RegistryNo,
                YearOfProduction = veh.YearOfProduction,  // Zmienna jest już typu int
                RentalFrom = veh.RentalFrom,  // Zmienna jest już typu DateTime
                RentalTo = veh.RentalTo,  // Zmienna jest już typu DateTime
                Prize = veh.Prize,  // Zmienna jest już typu decimal
                DriveType = veh.DriveType,
                Transmission = veh.Transmission,
                Description = veh.Description,
                Type = veh.Type,
                Rate = veh.Rate,  // Zmienna jest już typu decimal
                Localization = veh.Localization
            };
        }
    }
}
