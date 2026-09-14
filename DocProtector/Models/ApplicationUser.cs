using Microsoft.AspNetCore.Identity;

namespace DocProtector.Models
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Application User Full Name
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// User Creation Date and time
        /// </summary>
        public DateTime CreationAt { get; set; } = DateTime.UtcNow;
    }
}
