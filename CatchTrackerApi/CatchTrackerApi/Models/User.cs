using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CatchTrackerApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User"; // "User" або "Admin"

        [MaxLength(500)]
        public string RefreshToken { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Навігаційна властивість: 1 User -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
