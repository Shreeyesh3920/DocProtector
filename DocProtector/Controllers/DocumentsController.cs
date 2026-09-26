using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DocProtector.Controllers
{
    [Authorize]
    [Route("document")]
    [ApiController]
    public class DocumentsController : Controller
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService documentService)
        {
            _documentService = documentService;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is required.");
            }
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var document = await _documentService.UploadDocumentAsync(file, userId);
            return Ok(document);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserDocuments()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var documents = await _documentService.GetUserDocumentsAsync(userId);
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var document = await _documentService.GetDocumentByIdAsync(id, userId);

            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }
    }
}
