using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.AuthDTOs
{
    public class RefreshTokenDTO
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
