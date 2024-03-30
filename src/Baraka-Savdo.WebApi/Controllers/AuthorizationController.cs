using Baraka_Savdo.Service.Dtos.Auth;
using Baraka_Savdo.Service.Interfaces.Auth;

using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Baraka_Savdo.WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[EnableCors("AllowSpecificOrigin")]

public class AuthorizationController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthorizationController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
    {
        var result = await _authService.RegisterAsync(registerDto);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginDto loginDto)
    {
        var result = await _authService.LoginAsync(loginDto);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SendVerificationCodeAsync(string email)
    {
        var result = await _authService.SendVerificationCodeAsync(email);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> EmailComfirmationCodeAsync(int code)
    {
        var result = await _authService.EmailComfirmationCodeAsync(code);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var result = await _authService.ResetPasswordAsync(resetPasswordDto);
        return Ok(result);
    }
}