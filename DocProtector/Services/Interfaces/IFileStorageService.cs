namespace DocProtector.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<(Guid FileGuid, string PhysicalPath)> SaveFileAsync(IFormFile file);
        Task DeleteFileAsync(string physicalPath);
    }
}
