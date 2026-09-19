using DocProtector.Data;
using DocProtector.Models;
using DocProtector.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await context!.RefreshTokens.AddAsync(refreshToken);
            await context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string refreshToken)
        {
            RefreshToken? token= await context!.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken && x.IsRevoked == false);
            return token!;
        }

        public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            RefreshToken? token = await context!.RefreshTokens.SingleOrDefaultAsync(x => x.Token == refreshToken.Token && x.IsRevoked == false);
            token!.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
    }
}
