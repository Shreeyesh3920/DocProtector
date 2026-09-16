using DocProtector.DTOs;
using DocProtector.Models;
using DocProtector.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace DocProtector.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITokenService tokenService;
        public AuthService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, ITokenService _tokenService) { 
            userManager = _userManager;
            signInManager = _signInManager;
            tokenService = _tokenService;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginRequestDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(LoginResponseDTO loginResponse, string? token)> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null) 
                throw new ArgumentNullException(nameof(loginRequestDTO));

            ApplicationUser? user = await userManager.FindByEmailAsync(loginRequestDTO.Email);
            if (user == null) {
                return (new LoginResponseDTO()
                {
                    Succeeded = false,
                    Message = "Login failed.",
                    Errors = new List<string> { "Invalid email or password." }
                }, 
                null);
            }

            bool result = await userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            var Token = tokenService.GenerateToken(user);
            if (result)
            {
                return (new LoginResponseDTO()
                {   
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email!,
                    Succeeded = true,
                    Message = "Login successful."
                },
                Token);
            }

            return (new LoginResponseDTO()
            {
                Succeeded = false,
                Message = "Login failed. Please check your credentials and try again.",
                Errors = new List<string> { "Invalid email or password." }
            }, 
            null);
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
