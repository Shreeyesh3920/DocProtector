using DocProtector.DTOs;
using DocProtector.Models;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DocProtector.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService authService;
        public AuthenticationController(IAuthService _authService) {
            authService = _authService;
        }

        [HttpPost("register")]
        public async Task<RegisterResponseDTO> Register(RegisterRequestDTO registerRequestDTO) {
        return await authService.RegisterAsync(registerRequestDTO);
        }
    }
}
