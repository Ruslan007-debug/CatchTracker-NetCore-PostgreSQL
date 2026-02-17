using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.Models
{
    public class FishType
    {
        public int Id { get; set; }               // PK
        [MaxLength(100)]
        public string TypeName { get; set; }
        [MaxLength(100)]
        public string FavBait { get; set; }
        [MaxLength(100)]
        public double AvgWeight { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        // Навігаційна властивість: 1 FishType -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
