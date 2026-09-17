using DocProtector.DTOs.User;
using DocProtector.Models;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DocProtector.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CurrentUserResponseDTO?> GetCurrentUserAsync(string userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            return new CurrentUserResponseDTO
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!
            };
        }
    }
}