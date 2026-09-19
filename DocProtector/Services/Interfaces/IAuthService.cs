using DocProtector.DTOs;

namespace DocProtector.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDTO);
        public Task<(LoginResponseDTO loginResponse, string? accessToken, string? refreshToken)> LoginAsync(LoginRequestDTO loginRequestDTO);

    }
}
