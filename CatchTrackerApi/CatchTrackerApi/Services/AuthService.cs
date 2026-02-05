using CatchTrackerApi.DTOs.AuthDTOs;
using CatchTrackerApi.DTOs.UserDTOs;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Models;
using CatchTrackerApi.Mappers;

namespace CatchTrackerApi.Services
{
    public class AuthService: IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;
        private readonly int _accessTokenExpirationMinutes;

        private UserDTO MapToUserDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt
            };
        }

        public AuthService(IUserRepository userRepository, IJWTService jwtService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _accessTokenExpirationMinutes = int.Parse(configuration["JwtSettings:AccessTokenExpirationMinutes"] ?? "15");
        }

        public async Task<UserDTO> GetCurrentUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }
            return MapToUserDTO(user);
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid password.");
            }

            user.RefreshToken = _jwtService.GenerateRefreshToken();
            await _userRepository.UpdateUserAsync(user);

            var accessToken = _jwtService.GenerateAccessToken(user);
            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                TokenType = "Bearer",
                ExpiresIn = _accessTokenExpirationMinutes * 60,
                User = MapToUserDTO(user)
            };
        }

        public async Task<bool> LogoutAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.RefreshToken = string.Empty;
            await _userRepository.UpdateUserAsync(user);
            return true;
        }

        public async Task<UserDTO> PromoteToAdminAsync(int userId)
        {
            var userToAdmin = await _userRepository.GetUserByIdAsync(userId);
            if (userToAdmin == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            userToAdmin.Role = "Admin";
            await _userRepository.UpdateUserAsync(userToAdmin);
            return MapToUserDTO(userToAdmin);
        }

        public async Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
            {
                throw new KeyNotFoundException("Invalid refresh token.");
            }

            user.RefreshToken = _jwtService.GenerateRefreshToken();
            await _userRepository.UpdateUserAsync(user);

            var accessToken = _jwtService.GenerateAccessToken(user);

            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                TokenType = "Bearer",
                ExpiresIn = _accessTokenExpirationMinutes * 60,
                User = MapToUserDTO(user)
            };
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto)
        {
            var existindUserName = await _userRepository.GetUserByUsernameAsync(registerDto.Name);
            if (existindUserName != null)
            {
                throw new InvalidOperationException("Username is already taken.");
            }

            var existingUserEmail = await _userRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUserEmail != null)
            {
                throw new InvalidOperationException("Email is already registered.");
            }

            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Role = "User",
                RefreshToken = _jwtService.GenerateRefreshToken()
            };

            await _userRepository.CreateUserAsync(user);

            var accessToken = _jwtService.GenerateAccessToken(user);

            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken,
                TokenType = "Bearer",

                ExpiresIn = _accessTokenExpirationMinutes * 60,
                User = MapToUserDTO(user)
            };
        }
    }
}
