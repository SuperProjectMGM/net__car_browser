using search.api.DTOs.User;
using search.api.Models;

namespace search.api.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdDefaultAsync(string id);
    Task<User> CreateUserAsync(User userModel);
    Task<User?> UpdateUserAsync(string id, UpdateUserDto userDto);
    Task<User?> DeleteUserAsync(string id);
}