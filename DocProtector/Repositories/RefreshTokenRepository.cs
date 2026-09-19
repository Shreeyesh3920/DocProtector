using DocProtector.Data;
using DocProtector.Models;
using DocProtector.Repositories.Interfaces;

namespace DocProtector.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext? context;
        public RefreshTokenRepository(ApplicationDbContext _context)
        {
            context = _context;
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            await context.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();
        }

        public Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
