using DocProtector.Models;
using DocProtector.Repositories;
using DocProtector.Repositories.Interfaces;
using DocProtector.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace DocProtector.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IRefreshTokenRepository refreshTokenRepository;

        public TokenService(IConfiguration _configuration, IRefreshTokenRepository _refreshTokenRepository) {
            configuration = _configuration;
            refreshTokenRepository = _refreshTokenRepository;
        }

        /// <summary>
        /// Generates Access Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateAccessToken(ApplicationUser user)
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


        /// <summary>
        /// Generates Refresh Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<string> GenerateRefreshToken(ApplicationUser user)
        { 
            string refreshToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
            RefreshToken token = new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(10),
                IsRevoked = false
            };

            await refreshTokenRepository.AddRefreshTokenAsync(token);

            return refreshToken;
        }

    }
}
