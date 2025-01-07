using search.api.Models;

public static class UserMappers
{
    public static UserDto ToUserDto(this UserDetails userDetails)
    {
        return new UserDto
        {
            UserName = userDetails.UserName!,
            Email = userDetails.Email!,
            PhoneNumber = userDetails.PhoneNumber!,
            Name = userDetails.Name,
            Surname = userDetails.Surname,
            BirthDate = userDetails.BirthDate,
            DrivingLicenseNumber = userDetails.DrivingLicenseNumber,
            DrivingLicenseIssueDate = userDetails.DrivingLicenseIssueDate,
            DrivingLicenseExpirationDate = userDetails.DrivingLicenseExpirationDate,
            StreetAndNumber = userDetails.StreetAndNumber,
            PostalCode = userDetails.PostalCode,
            City = userDetails.City,
            PersonalNumber = userDetails.PersonalNumber,
            IdCardIssuedBy = userDetails.IdCardIssuedBy,
            IdCardIssueDate = userDetails.IdCardIssueDate,
            IdCardExpirationDate = userDetails.IdCardExpirationDate
        };
    }


    public static UserDetails ToUserDetails(this UserDto userDto)
    {
        return new UserDetails
        {
            UserName = userDto.UserName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Name = userDto.Name,
            Surname = userDto.Surname,
            BirthDate = userDto.BirthDate,
            DrivingLicenseNumber = userDto.DrivingLicenseNumber,
            DrivingLicenseIssueDate = userDto.DrivingLicenseIssueDate,
            DrivingLicenseExpirationDate = userDto.DrivingLicenseExpirationDate,
            StreetAndNumber = userDto.StreetAndNumber,
            PostalCode = userDto.PostalCode,
            City = userDto.City,
            PersonalNumber = userDto.PersonalNumber,
            IdCardIssuedBy = userDto.IdCardIssuedBy,
            IdCardIssueDate = userDto.IdCardIssueDate,
            IdCardExpirationDate = userDto.IdCardExpirationDate
        };
    }

    public static UserDetails ToUserDetails(this RegisterModel registerModel)
    {
        return new UserDetails
        {
            Email = registerModel.Email,
            UserName = registerModel.UserName,
            Name = registerModel.Name,
            Surname = registerModel.Surname,
            PhoneNumber = registerModel.PhoneNumber,
            BirthDate = registerModel.DateOfBirth,
            StreetAndNumber = registerModel.StreetAndNumber,
            PostalCode = registerModel.PostalCode,
            City = registerModel.City,
            DrivingLicenseNumber = registerModel.DrivingLicenseNumber,
            DrivingLicenseIssueDate = registerModel.DrivingLicenseIssueDate,
            DrivingLicenseExpirationDate = registerModel.DrivingLicenseExpirationDate,
            PersonalNumber = registerModel.PersonalNumber,
            IdCardIssuedBy = registerModel.IdCardIssuedBy,
            IdCardIssueDate = registerModel.IdCardIssueDate,
            IdCardExpirationDate = registerModel.IdCardExpirationDate
        };
    }
}