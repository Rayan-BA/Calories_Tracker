using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
  private readonly UserManager<User> _userManager;
  //private readonly SignInManager<User> _signInManager;
  private readonly ITokenService _tokenService;
  public AccountController(UserManager<User> userManager, ITokenService tokenService)
  {
    _userManager = userManager;
    //_signInManager = signInManager;
    _tokenService = tokenService;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
  {
    try
    {
      if (!ModelState.IsValid)
        return BadRequest(ModelState);

      var user = new User
      {
        UserName = registerDTO.Username,
        Email = registerDTO.Email
      };

      var userResult = await _userManager.CreateAsync(user, registerDTO.Password);
      if (!userResult.Succeeded)
        return StatusCode(500, userResult.Errors);

      var roleResult = await _userManager.AddToRoleAsync(user, "User");
      if (!roleResult.Succeeded)
        return StatusCode(500, roleResult.Errors);

      return Ok(new { message = "User created", Token = _tokenService.CreateToken(user) });
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
  {
    if (!ModelState.IsValid)
      return BadRequest(ModelState);

    var user = await _userManager.FindByNameAsync(loginDTO.Username);
    if (user == null)
      return Unauthorized("Invalid username");

    // could use _signInManager.PasswordSignInAsync() instead, if account lockout/2FA/email confirmation is needed later.
    var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
    if (!passwordCheck)
      return Unauthorized("Invalid password");

    return Ok(new { message = "Logged in", Token = _tokenService.CreateToken(user) });
  }
}
