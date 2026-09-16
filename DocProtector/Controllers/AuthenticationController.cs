using DocProtector.DTOs;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocProtector.Controllers
{
    [ApiController]
    [Route("user")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService authService;
        public AuthenticationController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpPost("register")]
        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequestDTO)
        {
            return await authService.RegisterAsync(registerRequestDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var result = await authService.LoginAsync(loginRequestDTO);
            if(!result.loginResponse.Succeeded)
                return Unauthorized(result.loginResponse.Message);

            Response.Cookies.Append(
                "accessToken", result.token!,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(1)
                }
                );

            return Ok(result.loginResponse);
        }
    }
}