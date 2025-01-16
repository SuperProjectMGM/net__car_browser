using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                Brand = dto.Brand,
                Model = dto.Model,
                SerialNo = dto.SerialNo,
                Vin = dto.Vin,
                RegistryNo = dto.RegistryNo,
                YearOfProduction = dto.YearOfProduction, 
                Price = dto.Price,
                DriveType = dto.DriveType,
                Transmission = dto.Transmission,
                Description = dto.Description,
                Type = dto.Type,
                Rate = dto.Rate,
                Localization = dto.Localization,
                RentalFirmName = dto.RentalFirmName,
                PhotoUrl = dto.PhotoUrl
            };
        }

        public static VehicleOurDto VehicleToVehicleOurDto(this Vehicle veh)
        {
            return new VehicleOurDto
            {
                Brand = veh.Brand,
                Model = veh.Model,
                SerialNo = veh.SerialNo,
                Vin = veh.Vin,
                RegistryNo = veh.RegistryNo,
                YearOfProduction = veh.YearOfProduction,
                Price = veh.Price, 
                DriveType = veh.DriveType,
                Transmission = veh.Transmission,
                Description = veh.Description,
                Type = veh.Type,
                Rate = veh.Rate,  
                Localization = veh.Localization,
                PhotoUrl = veh.PhotoUrl,
                RentalFirmName = veh.RentalFirmName
            };
        }

        public static VehicleOurDto VehicleKEJDtoToOurDto(this VehicleKEJDto kejDto)
        {
            return new VehicleOurDto
            {
                Brand = kejDto.Producer,
                Model = kejDto.Model,
                YearOfProduction = kejDto.YearOfProduction,
                Price = 999999,
                //Vin = string.Join("_", [$"{kejDto.RentalService}".ToUpper(), kejDto.Id.ToString()]),
                Localization = "Warsaw", 
                RentalFirmName = "KEJCO",
                Type = kejDto.Type.ToString(),
                Rate = 11
            };
        }

        public static string KEJParseLocalization(string loc)
        {
            string tmp = loc.Split(" ")[4];
            tmp.Remove(tmp.Length - 1);
            return tmp;
        }
    }

}
