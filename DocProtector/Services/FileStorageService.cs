using DocProtector.Services.Interfaces;

namespace DocProtector.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment env;

        public FileStorageService(IWebHostEnvironment environment)
        {
            env = environment;
        }

        public async Task<(Guid FileGuid, string PhysicalPath)> SaveFileAsync(IFormFile file)
        {
            Guid fileGuid = Guid.NewGuid();
            string uploadFolder = Path.Combine(env.ContentRootPath, "UploadedFiles", fileGuid.ToString());

            Directory.CreateDirectory(uploadFolder);

            string filePath = Path.Combine(uploadFolder, file.FileName);
            await using FileStream stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);
            return (fileGuid, filePath);
        }

        public Task DeleteFileAsync(string physicalPath)
        {
            if (File.Exists(physicalPath)) 
            {
                File.Delete(physicalPath);
            }
            return Task.CompletedTask;
        }

       
    }
}
