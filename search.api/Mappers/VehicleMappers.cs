using search.api.DTOs;
using search.api.Models;

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
                YearOfProduction = dto.YearOfProduction,
                Type = dto.Type,
                RentalFirm = new RentalFirm() // TODO: now it's emty, nut we should change it
            };
        }
    
        public static VehicleOurDto VehicletoVehicleOurDto(this Vehicle veh)
        {
            return new VehicleOurDto
            {
                CarId = veh.CarId,
                Brand = veh.Brand,
                Model = veh.Model,
                YearOfProduction = veh.YearOfProduction,
                Type = veh.Type
            };
        }
    
    }
}

