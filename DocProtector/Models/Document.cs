using System.ComponentModel.DataAnnotations;

namespace DocProtector.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public Guid FileGuid { get; set; }

        [Required]
        public string FileName { get; set; } = string.Empty;

        [Required]
        public string FileType { get; set; } = string.Empty;

        [Required]
        public string PhysicalPath { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Uploaded";
    }
}
