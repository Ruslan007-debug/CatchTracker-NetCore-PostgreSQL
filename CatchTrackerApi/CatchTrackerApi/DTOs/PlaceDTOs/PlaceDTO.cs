using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.DTOs.PlaceDTOs
{
    public class PlaceDTO
    {
        public int Id { get; set; }              
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        public string BiggestTrophy { get; set; } = string.Empty;
        [MaxLength(10)]
        public double WaterTemp { get; set; }
        [MaxLength(200)]
        public string ImgUrl { get; set; } = string.Empty;
        [MaxLength(2000)]
        public string Description { get; set; } = string.Empty;
        public int PondId { get; set; }
    }
}
