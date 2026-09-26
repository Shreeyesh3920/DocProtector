using DocProtector.Models;

namespace DocProtector.Repositories.Interfaces
{
    public interface IDocumentRepository
    {
        Task AddAsync(Document document);
        Task<Document?> GetByIdAsync(int id);
        Task<List<Document>> GetByUserIdAsync(string userId);
    }
}
