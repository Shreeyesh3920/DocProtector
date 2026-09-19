using DocProtector.DTOs;
using DocProtector.DTOs.Dashboard;
using DocProtector.Services;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocProtector.Controllers
{
    [ApiController]
    [Route("user")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService authService;
        private readonly IUserService userService;

        public AuthenticationController(IAuthService _authService, IUserService _userService)
        {
            authService = _authService;
            userService = _userService;
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

            Response.Cookies.Append("access_token", result.accessToken!,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                });

            Response.Cookies.Append("refresh_token", result.refreshToken!,
                new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = true,
                    Expires = DateTimeOffset.UtcNow.AddDays(10)
                });

            return Ok(result.loginResponse);
        }


        /// <summary>
        /// Logout: server deletes/clears access_token cookie
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token",
                    new CookieOptions
                    {
                        Path = "/",
                        Secure = true,
                        SameSite = SameSiteMode.None
                    }
                );

            Response.Cookies.Delete("refresh_token",
                    new CookieOptions
                    {
                        Path = "/",
                        Secure = true,
                        SameSite = SameSiteMode.None
                    }
                );

            return Ok(new
            {
                message = "Logout successful."
            });
        }

        [Authorize]
        [HttpGet("checkAuthentication")]
        public async Task<IActionResult> CheckAuthentication()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized();

            var user = await userService.GetCurrentUserAsync(userId);

            if (user == null)
                return Unauthorized();

            return Ok(user);
        }
    }
}