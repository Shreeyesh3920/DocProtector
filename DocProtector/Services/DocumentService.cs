using DocProtector.Models;
using DocProtector.Repositories.Interfaces;
using DocProtector.Services.Interfaces;

namespace DocProtector.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IFileStorageService _fileStorageService;

        public DocumentService(IDocumentRepository documentRepository, IFileStorageService fileStorageService)
        {
            _documentRepository = documentRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<Document> UploadDocumentAsync(IFormFile file, string userId)
        {
            var (fileGuid, physicalPath) = await _fileStorageService.SaveFileAsync(file);

            var document = new Document
            {
                UserId = userId,
                FileGuid = fileGuid,
                FileName = file.FileName,
                FileType = file.ContentType,
                PhysicalPath = physicalPath,
                FileSize = file.Length,
                UploadedAt = DateTime.UtcNow,
                Status = "Uploaded"
            };

            await _documentRepository.AddAsync(document);

            return document;
        }

        public async Task<Document?> GetDocumentByIdAsync(int documentId, string userId)
        {
            var document = await _documentRepository.GetByIdAsync(documentId);

            if (document == null)
                return null;

            if (document.UserId != userId)
                return null;

            return document;
        }

        public async Task<List<Document>> GetUserDocumentsAsync(string userId)
        {
            return await _documentRepository.GetByUserIdAsync(userId);
        }

        
    }
}
