using DocProtector.Models;

namespace DocProtector.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        public Task AddRefreshTokenAsync(RefreshToken refreshToken);
        public Task<RefreshToken> GetRefreshTokenAsync(string refreshToken);
        public Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
    }
}
