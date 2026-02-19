using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.PlaceDTOs
{
    public class UpdatePlaceDTO
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        [Required]
        public string BiggestTrophy { get; set; } = string.Empty;
        [MaxLength(10)]
        public double WaterTemp { get; set; }
        [MaxLength(200)]
        [Required]
        public string ImgUrl { get; set; } = string.Empty;
        [MaxLength(2000)]
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
