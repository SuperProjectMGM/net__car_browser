using Microsoft.AspNetCore.Mvc;
using search.api.Data;
using search.api.DTOs.User;
using search.api.Mappers;
using search.api.Models;

namespace search.api.Controllers;

[Route("search.api/User")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Default means that narrowed User model is returned.
    /// Name, Surname, Email and Phone number
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public IActionResult GetAllDefault()
    {
        var users = _context.Users.ToList()
            .Select(s => s.ToUserDefaultDto());
        return Ok(users);
    }
    
    
    /// <summary>
    /// Default means that narrowed User model is returned.
    /// Name, Surname, Email and Phone number
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>

    [HttpGet("{id}")]
    public IActionResult GetByIdDefault([FromRoute] string id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
            return NotFound();

        return Ok(user.ToUserDefaultDto());
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] CreateUserDto userDto)
    {
        var userModel = userDto.CreateUserFromDefPost();
        _context.Users.Add(userModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetByIdDefault), new { id = userModel.Id },userModel.ToUserDefaultDto());
    }
    
}