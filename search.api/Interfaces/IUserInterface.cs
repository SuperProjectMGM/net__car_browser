using search.api.DTOs.User;
using search.api.Models;

namespace search.api.Interfaces;

public interface IUserInterface
{
    public Task<List<User>> GetAllAsync();
    public Task<User?> GetByIdDefaultAsync(string id);
    public Task<User> CreateUserAsync(User userModel);
    public Task<User?> UpdateUserAsync(string id, UpdateUserDto userDto);
    public Task<User?> DeleteUserAsync(string id);
}