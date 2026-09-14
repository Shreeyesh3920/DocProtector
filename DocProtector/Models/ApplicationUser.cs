using Microsoft.AspNetCore.Identity;

namespace DocProtector.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;

        public DateTime CreationAt { get; set; } = DateTime.UtcNow;
    }
}
