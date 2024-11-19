using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using search.api.Data;
using search.api.DTOs.User;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Repositories;

public class UserRepository : IUserInterface
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<UserDetails>> GetAllAsync()
    {
        return await _context.UsersDetails.ToListAsync();
    }

    public async Task<UserDetails?> GetByIdDefaultAsync(string id)
    {
        return await _context.UsersDetails.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<UserDetails> CreateUserAsync(UserDetails userDetailsModel)
    {
        await _context.UsersDetails.AddAsync(userDetailsModel);
        await _context.SaveChangesAsync();
        return userDetailsModel;
    }

    public async Task<UserDetails?> UpdateUserAsync(string id, UpdateUserDto userDto)
    {
        var existingUser = await _context.UsersDetails.FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
            return null;
        
        existingUser.Login = userDto.Login;
        existingUser.Password = userDto.Password;

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task<UserDetails?> DeleteUserAsync(string id)
    {
        var userModel = await _context.UsersDetails.FirstOrDefaultAsync(x => x.Id == id);
        if (userModel == null)
            return null;
        
        _context.UsersDetails.Remove(userModel);
        await _context.SaveChangesAsync();
        return userModel;
    }
}