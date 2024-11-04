using search.api.DTOs.User;
using search.api.Models;

namespace search.api.Mappers;

public static class UserMappers
{
    public static UserDefaultDto ToUserDefaultDto(this User userModel)
    {
        return new UserDefaultDto
        {
            Name = userModel.Name,
            Surname = userModel.Surname,
            Email = userModel.Email,
            PhoneNumber = userModel.PhoneNumber
        };
    }
}