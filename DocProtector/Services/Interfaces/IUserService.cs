using DocProtector.DTOs.User;

namespace DocProtector.Services.Interfaces
{
    public interface IUserService
    {
        Task<CurrentUserResponseDTO?> GetCurrentUserAsync(string userId);
    }
}
