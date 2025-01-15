using Microsoft.AspNetCore.Identity;
using search.api.Models;

namespace search.api.Interfaces;

public interface IUserInfoInterface
{
    public int? ReturnIdFromToken(string token);
    public Task<UserDetails?> FindUserById(int id);
    public Task<int> ChangeDataAboutUser();
}