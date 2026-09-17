using DocProtector.DTOs.User;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocProtector.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User ID not found");
            CurrentUserResponseDTO? currentUser = userService.GetCurrentUserAsync(userId).Result;
            
            if (currentUser == null)
                return NotFound("User not found");
            
            return Ok(currentUser);
        }
    }
}
