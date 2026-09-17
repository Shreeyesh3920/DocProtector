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

        /// <summary>
        /// Login: server creates access_token cookie
        /// </summary>
        /// <param name="loginRequestDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            var result = await authService.LoginAsync(loginRequestDTO);
            if(!result.loginResponse.Succeeded)
                return Unauthorized(result.loginResponse.Message);

            Response.Cookies.Append(
                "access_token", result.token!,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                }
                );

            return Ok(result.loginResponse);
        }


        /// <summary>
        /// Logout: server deletes/clears access_token cookie
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");

            return Ok(new
            {
                message = "Logout successful."
            });
        }
    }
}