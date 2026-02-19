using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.Models
{
    public class Place
    {
        public int Id { get; set; }               // PK
        [MaxLength(200)]
        [Required] public string Name { get; set; } = string.Empty;
        [MaxLength(200)]
        [Required] public string BiggestTrophy { get; set; } = string.Empty;
        [MaxLength(10)]
        public double WaterTemp { get; set; }
        [MaxLength(200)]
        [Required] public string ImgUrl { get; set; } = string.Empty;
        [MaxLength(2000)]
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int PondId { get; set; }


        // Навігаційна властивість: 1 Place -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
