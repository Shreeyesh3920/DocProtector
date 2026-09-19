using DocProtector.DTOs.Dashboard;
using DocProtector.Services;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocProtector.Controllers
{
    [Authorize]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            this.dashboardService = dashboardService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard() {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized("User ID not found");

            DashboardResponseDTO? dashboard = await dashboardService.GetDashboardAsync(userId);
            if (dashboard == null)
                return NotFound("Dashboard not found");

            return Ok(dashboard);
        }


    }
}
