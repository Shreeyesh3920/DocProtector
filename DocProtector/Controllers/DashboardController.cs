using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocProtector.Controllers
{
    [ApiController]
    public class DashboardController : Controller
    {
        public DashboardController() { }

        [Authorize]
        [HttpGet("api/dashboard")]
        public IActionResult Index()
        {
            return Ok("Dashboard");
        }
    }
}
