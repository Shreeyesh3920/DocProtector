using DocProtector.DTOs.Dashboard;
using DocProtector.Services.Interfaces;

namespace DocProtector.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserService _userService;
        public DashboardService(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<DashboardResponseDTO?> GetDashboardAsync(string userId)
        {
            var user = await _userService.GetCurrentUserAsync(userId);

            if (user == null)
                return null;

            return new DashboardResponseDTO
            {
                User = user,
            };
        }
    }
}
