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
        public AuthService(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager ) { 
            userManager = _userManager;
            signInManager = _signInManager;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="loginRequestDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            if (loginRequestDTO == null) 
                throw new ArgumentNullException(nameof(loginRequestDTO));


            ApplicationUser? user = await userManager.FindByEmailAsync(loginRequestDTO.Email);
            if (user == null) { 
                return new LoginResponseDTO() { 
                    Succeeded = false,
                    Message = "Login failed.",
                    Errors = new List<string> { "Invalid email or password." }
                };
            }

            await signInManager.PasswordSignInAsync(user, loginRequestDTO.Password, true, lockoutOnFailure: false);


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
