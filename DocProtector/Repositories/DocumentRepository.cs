using DocProtector.Data;
using DocProtector.Models;
using DocProtector.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DocProtector.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Document document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task<Document?> GetByIdAsync(int id)
        {
            return await _context.Documents.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Document>> GetByUserIdAsync(string userId)
        {
            return await _context.Documents.Where(d => d.UserId == userId).ToListAsync();
        }
    }
}
