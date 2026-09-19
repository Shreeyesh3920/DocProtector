using DocProtector.Models;
using System.CodeDom.Compiler;

namespace DocProtector.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateAccessToken(ApplicationUser user);
        public Task<string> GenerateRefreshToken(ApplicationUser user);
    }
}
