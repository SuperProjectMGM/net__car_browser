using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using search.api.Interfaces;
using search.api.Models;



namespace search.api.Controllers;

[Route("search.api/[controller]")]
[ApiController]
public class AuthenticateController: ControllerBase
{
    private IAuthInterface _authRepository; 
    public AuthenticateController(IAuthInterface authRepo)
    {
        _authRepository = authRepo;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        bool userExists = await _authRepository.CheckIfUserExist(model.ToUserDetails());
        if (userExists)
        {
            return Conflict("User with such email does already exist!");
        }
        
        var result = await _authRepository.CreateNewUser(model, model.ToUserDetails());
        if (!result.Succeeded)
        {
            var errorMessages = string.Join(", ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
            return BadRequest($"User creation failed: {errorMessages}");
        }

        return Ok();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var claims = await _authRepository.CheckLogin(model);
        if (!(claims is null))
        {
            var token = _authRepository.GetToken(claims);
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
        return Unauthorized();
    }

    [HttpGet]
    [Route("login/google-login")]
    public IActionResult GoogleLogin()
    {
        var properties = _authRepository.AddGoogleOptions();
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet]
    [Route("login/google-callback")]
    public async Task<IActionResult> GoogleCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!authenticateResult.Succeeded)
            return BadRequest("Not authenticated by google");
        var claims = authenticateResult.Principal?.Claims;
        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var claimsRet = await _authRepository.GoogleCallbackFunction(email!);
        if (claimsRet is null)
        return Unauthorized("Can't crate user");
        var token = _authRepository.GetToken(claimsRet!);
        // TODO: Change hardcoded values
        var redirectUrl = $"http://localhost:4199/auth/google-callback?token={new JwtSecurityTokenHandler().WriteToken(token)}";
        return Redirect(redirectUrl);
    }
}