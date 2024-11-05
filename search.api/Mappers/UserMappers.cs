using System.Globalization;
using NanoidDotNet;
using search.api.DTOs.User;
using search.api.Models;

namespace search.api.Mappers;

public static class UserMappers
{
    public static UserDefaultDto ToUserDefaultDto(this User userModel)
    {
        return new UserDefaultDto
        {
            Id = userModel.Id,
            Login = userModel.Login,
            Name = userModel.Name,
            Surname = userModel.Surname,
            Email = userModel.Email,
            PhoneNumber = userModel.PhoneNumber
        };
    }

    public static User CreateUserFromDefPost(this CreateUserDto userDto)
    {
        return new User
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