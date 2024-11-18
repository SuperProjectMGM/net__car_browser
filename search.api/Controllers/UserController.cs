using Microsoft.AspNetCore.Mvc;
using search.api.Data;
using search.api.DTOs.User;
using search.api.Interfaces;
using search.api.Mappers;
using search.api.Models;
using search.api.Repositories;

namespace search.api.Controllers;

[Route("search.api/User")]
[ApiController]
public class UserController : ControllerBase
{
    //private readonly AppDbContext _context;
    private readonly IUserInterface _userRepo;
    public UserController(AppDbContext context, IUserInterface userRepo)
    {
        //_context = context;
        _userRepo = userRepo;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDefault()
    {
        var users = await _userRepo.GetAllAsync();
        var usersDto = users.Select(s => s.ToUserDefaultDto());
        return Ok(usersDto);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdDefault([FromRoute] string id)
    {
        var user = await _userRepo.GetByIdDefaultAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user.ToUserDefaultDto());
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var userModel = userDto.CreateUserFromDefPost();
        await _userRepo.CreateUserAsync(userModel);
        return CreatedAtAction(nameof(GetByIdDefault), new { id = userModel.Id },userModel.ToUserDefaultDto());
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UpdateUserDto userDto)
    {
        var userModel = await _userRepo.UpdateUserAsync(id, userDto);
        if (userModel == null)
            return NotFound();
        return Ok(userModel.ToUserDefaultDto());
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id)
    {
        var userModel = await _userRepo.DeleteUserAsync(id);
        if (userModel == null)
            return NotFound();
        return NoContent();
    }
}