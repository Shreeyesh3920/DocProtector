using DocProtector.Models;
using System.CodeDom.Compiler;

namespace DocProtector.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(ApplicationUser user);
    }
}
