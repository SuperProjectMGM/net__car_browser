using System.Globalization;
using NanoidDotNet;
using search.api.DTOs.User;
using search.api.Models;

namespace search.api.Mappers;

public static class UserMappers
{
    public static UserDefaultDto ToUserDefaultDto(this UserDetails userDetailsModel)
    {
        return new UserDefaultDto
        {
            Id = userDetailsModel.Id,
            Login = userDetailsModel.Login,
            Name = userDetailsModel.Name,
            Surname = userDetailsModel.Surname,
            Email = userDetailsModel.Email,
            PhoneNumber = userDetailsModel.PhoneNumber
        };
    }

    public static UserDetails CreateUserFromDefPost(this CreateUserDto userDto)
    {
        return new UserDetails
        {
            Id = Nanoid.Generate(Nanoid.Alphabets.LowercaseLettersAndDigits, 10),
            Login = userDto.Login,
            Password = userDto.Password,
            Name = userDto.Name,
            Surname = userDto.Surname,
            BirthDate = userDto.BirthDate,
            DateOfReceiptOfDrivingLicense = userDto.DateOfReceiptOfDrivingLicense,
            PersonalNumber = userDto.PersonalNumber,
            LicenceNumber = userDto.LicenceNumber,
            Email = userDto.Email,
            Address = userDto.Address,
            PhoneNumber = userDto.PhoneNumber
        };
    }
}