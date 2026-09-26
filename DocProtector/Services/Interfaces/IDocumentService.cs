using DocProtector.Models;

namespace DocProtector.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> UploadDocumentAsync(IFormFile file, string userId);

        Task<Document?> GetDocumentByIdAsync(int documentId, string userId);

        Task<List<Document>> GetUserDocumentsAsync(string userId);
    }
}
