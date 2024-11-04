using Microsoft.AspNetCore.Mvc;
using search.api.Data;
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

    [HttpGet]

    public IActionResult GetDefaultAll()
    {
        var users = _context.Users.ToList()
            .Select(s => s.ToUserDefaultDto());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetByDefaultId([FromRoute] int id)
    {
        var user = _context.Users.Find(id);

        if (user == null)
            return NotFound();

        return Ok(user.ToUserDefaultDto());
    }
    
}