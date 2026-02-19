using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.Models
{
    public class FishType
    {
        public int Id { get; set; }               // PK
        [MaxLength(100)]
        [Required] public string TypeName { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required] public string FavBait { get; set; } = string.Empty;
        [MaxLength(100)]
        public double AvgWeight { get; set; }
        [MaxLength(1000)]
        [Required] public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        [Required]
        public string? ImageUrl { get; set; }
        public bool IsPredatory { get; set; }

        // Навігаційна властивість: 1 FishType -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
