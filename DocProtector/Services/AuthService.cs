using DocProtector.DTOs;
using DocProtector.Models;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DocProtector.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        public AuthService(UserManager<ApplicationUser> _userManager) { 
            userManager = _userManager;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginRequestDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="registerRequestDTO"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerRequestDTO)
        {
            if (registerRequestDTO == null) 
                throw new ArgumentNullException(nameof(registerRequestDTO));

            var existingUser = await userManager.FindByEmailAsync(registerRequestDTO.Email);
            if(existingUser != null)
                return new RegisterResponseDTO
                {
                    Succeeded = false,
                    Message = "A user with this email already exists."
                };

            ApplicationUser user = new ApplicationUser()
            {
                UserName = registerRequestDTO.Email,
                FullName = registerRequestDTO.FullName,
                Email = registerRequestDTO.Email
            };
            IdentityResult result = await userManager.CreateAsync(user, registerRequestDTO.Password);

            if (!result.Succeeded) 
            {
                return new RegisterResponseDTO()
                {
                    Succeeded = false,
                    Message = "User registration failed.",
                    Errors = result.Errors.Select(e=> e.Description).ToList()
                };
            }

            return new RegisterResponseDTO()
            {
                Succeeded = true,
                Message = "User registered successfully."
            };
        }
    }
}
