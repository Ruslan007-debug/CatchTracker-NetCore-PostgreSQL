using CatchTrackerApi.DTOs.AuthDTOs;
using CatchTrackerApi.DTOs.UserDTOs;

namespace CatchTrackerApi.Interfaces.ServiceInterfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<AuthResponseDTO> RefreshTokenAsync(string refreshToken);
        Task<bool> LogoutAsync(string refreshToken);
        Task<UserDTO> GetCurrentUserAsync(int userId);
        Task<UserDTO> PromoteToAdminAsync(int userId);
    }
}
