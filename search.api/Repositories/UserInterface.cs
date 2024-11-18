using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using search.api.Data;
using search.api.DTOs.User;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Repositories;

public class UserInterface : IUserInterface
{
    private readonly AppDbContext _context;

    public UserInterface(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdDefaultAsync(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User> CreateUserAsync(User userModel)
    {
        await _context.Users.AddAsync(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }

    public async Task<User?> UpdateUserAsync(string id, UpdateUserDto userDto)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
            return null;
        
        existingUser.Login = userDto.Login;
        existingUser.Password = userDto.Password;

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> DeleteUserAsync(string id)
    {
        var userModel = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (userModel == null)
            return null;
        
        _context.Users.Remove(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }
}