using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.FishTypeDTOs
{
    public class CreateFishTypeDTO
    {
        [MaxLength(100)]
        public string TypeName { get; set; } = string.Empty;
        [MaxLength(100)]
        public string FavBait { get; set; } = string.Empty;
        [MaxLength(100)]
        public double AvgWeight { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? ImageUrl { get; set; }
    }
}
