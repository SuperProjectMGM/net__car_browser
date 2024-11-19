using search.api.DTOs.User;
using search.api.Models;

namespace search.api.Interfaces;

public interface IUserInterface
{
    public Task<List<UserDetails>> GetAllAsync();
    public Task<UserDetails?> GetByIdDefaultAsync(string id);
    public Task<UserDetails> CreateUserAsync(UserDetails userDetailsModel);
    public Task<UserDetails?> UpdateUserAsync(string id, UpdateUserDto userDto);
    public Task<UserDetails?> DeleteUserAsync(string id);
}