using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using search.api.Interfaces;
using search.api.Models;

namespace search.api.Controllers;


[Route("search.api/[controller]")]
[ApiController]
public class UserInfoController: ControllerBase
{
    private readonly IUserInfoInterface _userRepository;
    public UserInfoController(IConfiguration configuration, AuthDbContext context, IUserInfoInterface userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    [Route("user-info")]
    public async Task<IActionResult> UserInfo([FromQuery] string token)
    {
        var id = _userRepository.ReturnIdFromToken(token);
        if (id is null)
        {
            return NotFound("Something is wrong with token!");
        }
        var user = await _userRepository.FindUserById(id.Value);
        if (user is null)
        {
            return NotFound("User doesn't exist!");
        }
        return Ok(user.ToUserDto());
    }

    [HttpPut]
    [Route("change-user-info")]
    public async Task<IActionResult> ChangeUserInfo([FromQuery] string token, [FromBody] UserDto userDto)
    {
        var id = _userRepository.ReturnIdFromToken(token);
        if (id is null)
        {
            return NotFound("Somethins is wrong with token!");
        }
        var user = await _userRepository.FindUserById(id.Value);
        if (user is null)
        {
            return NotFound("User doesn't exist!");
        }
        userDto.ToUserDetails(user);
        await _userRepository.ChangeDataAboutUser();
        return Content("Data changed", "text/plain");
    }
}

