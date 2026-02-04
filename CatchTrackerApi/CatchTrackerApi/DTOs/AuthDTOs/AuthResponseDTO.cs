using CatchTrackerApi.DTOs.UserDTOs;

namespace CatchTrackerApi.DTOs.AuthDTOs
{
    public class AuthResponseDTO
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; } // в секундах
        public UserDTO User { get; set; } = null!;
    }
}
