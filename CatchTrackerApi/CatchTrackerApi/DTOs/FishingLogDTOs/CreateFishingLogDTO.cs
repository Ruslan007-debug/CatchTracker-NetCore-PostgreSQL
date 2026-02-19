using CatchTrackerApi.DTOs.FishTypeDTOs;
using CatchTrackerApi.DTOs.PlaceDTOs;
using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.FishingLogDTOs
{
    public class CreateFishingLogDTO
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
