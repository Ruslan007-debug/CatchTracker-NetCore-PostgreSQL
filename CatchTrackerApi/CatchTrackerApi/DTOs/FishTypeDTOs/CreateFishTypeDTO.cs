using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.FishTypeDTOs
{
    public class CreateFishTypeDTO
    {
        [MaxLength(100)]
        [Required]
        public string TypeName { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public string FavBait { get; set; } = string.Empty;
        [MaxLength(100)]
        [Required]
        public double AvgWeight { get; set; }
        [MaxLength(1000)]
        [Required]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        [Required]
        public string? ImageUrl { get; set; }
        [Required]
        public bool IsPredatory { get; set; }
    }
}
