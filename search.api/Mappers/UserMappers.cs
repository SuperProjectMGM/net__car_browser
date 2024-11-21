using search.api.Models;

public static class UserMappers
{
    public static UserDto ToUserDto(this UserDetails userDetails)
    {
        return new UserDto
        {
            UserName = userDetails.UserName,
            Email = userDetails.Email,
            PhoneNumber = userDetails.PhoneNumber,
            Name = userDetails.Name,
            Surname = userDetails.Surname,
            BirthDate = userDetails.BirthDate,
            DrivingLicenseNumber = userDetails.DrivingLicenseNumber,
            DrivingLicenseIssueDate = userDetails.DrivingLicenseIssueDate,
            DrivingLicenseExpirationDate = userDetails.DrivingLicenseExpirationDate,
            AddressStreet = userDetails.AddressStreet,
            PostalCode = userDetails.PostalCode,
            City = userDetails.City,
            IdPersonalNumber = userDetails.IdPersonalNumber,
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
            AddressStreet = userDto.AddressStreet,
            PostalCode = userDto.PostalCode,
            City = userDto.City,
            IdPersonalNumber = userDto.IdPersonalNumber,
            IdCardIssuedBy = userDto.IdCardIssuedBy,
            IdCardIssueDate = userDto.IdCardIssueDate,
            IdCardExpirationDate = userDto.IdCardExpirationDate
        };
    }

    public static void ToUserDetails(this UserDto userDto, UserDetails user)
    {
        user.UserName = userDto.UserName;
        user.Email = userDto.Email;
        user.PhoneNumber = userDto.PhoneNumber;
        user.Name = userDto.Name;
        user.Surname = userDto.Surname;
        user.BirthDate = userDto.BirthDate;
        user.DrivingLicenseNumber = userDto.DrivingLicenseNumber;
        user.DrivingLicenseIssueDate = userDto.DrivingLicenseIssueDate;
        user.DrivingLicenseExpirationDate = userDto.DrivingLicenseExpirationDate;
        user.AddressStreet = userDto.AddressStreet;
        user.PostalCode = userDto.PostalCode;
        user.City = userDto.City;
        user.IdPersonalNumber = userDto.IdPersonalNumber;
        user.IdCardIssuedBy = userDto.IdCardIssuedBy;
        user.IdCardIssueDate = userDto.IdCardIssueDate;
        user.IdCardExpirationDate = userDto.IdCardExpirationDate;
    }
}