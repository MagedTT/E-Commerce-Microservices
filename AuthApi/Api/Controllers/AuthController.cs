using AuthAPI.Core.Dtos;
using AuthAPI.Core.Interfaces;
using CoreLib.Interfaces;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{
    private readonly IUser _users;
    public AuthController(IUser users)
    {
        _users = users;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseMessage>> Register(AppUserDto appUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _users.RegisterAsync(appUserDto);

        if (result.Status)
            return Ok(result);

        return BadRequest(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ResponseMessage>> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _users.LoginAsync(loginDto);

        if (result.Status)
            return Ok(result);

        return BadRequest(result);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<GetUserDto>> GetUserAsync(int id)
    {
        if (id <= 0)
            return BadRequest("Invalid user Id");

        var user = await _users.GetUserAsync(id);

        if (user.Id > 0)
            return Ok(user);

        return NotFound(user);
    }

}