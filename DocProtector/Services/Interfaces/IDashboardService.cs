using DocProtector.DTOs.Dashboard;

namespace DocProtector.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResponseDTO?> GetDashboardAsync(string userId);
    }
}
