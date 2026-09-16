using DocProtector.Models;
using DocProtector.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DocProtector.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration configuration;
        public TokenService(IConfiguration _configuration) { configuration = _configuration; }
        public string GenerateToken(ApplicationUser user)
        {
            string? secrete = configuration["Jwt:Secrete"];
            var symmericSecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secrete));
            var credentials = new SigningCredentials(symmericSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Application-User"),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Now.AddMinutes(10)
            );

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
