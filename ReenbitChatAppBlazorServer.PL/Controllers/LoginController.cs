using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReenbitChatAppBlazorServer.BLL.Services.Interfaces;
using ReenbitChatAppBlazorServer.Domain.DTOs;

namespace ReenbitChatAppBlazorServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly IAuthJwtService _authService;

    public LoginController(IAuthJwtService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO userData)
    {
        try
        {
            var user = await _authService.Auth(userData);
            var token = _authService.GenerateJwtToken(user);
            return Ok(token);
        }
        catch
        {
            return NotFound("User not found");
        }
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] LoginDTO userData)
    {
        var regState = await _authService.Registration(userData);

        if (regState.UserNameIsAlreadyExist)
            return BadRequest();

        return regState.Succeeded ? Ok() : StatusCode(500);
    }

    [Authorize]
    [HttpGet("GetAuthorize")]
    public IActionResult GetAuthorize()
    {
        return Ok();
    }
}
