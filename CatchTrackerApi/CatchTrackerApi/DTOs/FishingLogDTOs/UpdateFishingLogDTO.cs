using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.FishingLogDTOs
{
    public class UpdateFishingLogDTO
    {
        [Required]
        public int PlaceId { get; set; }
        [Required]
        public int FishTypeId { get; set; }
        [Required]
        public string Trophy { get; set; } = string.Empty;
        [Required]
        public double Weight { get; set; }
        [Required]
        public string Bait { get; set; } = string.Empty;
        [Required]
        public double Distance { get; set; }
    }
}
