using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CatchTrackerApi.Models
{
    public class Place
    {
        public int Id { get; set; }               // PK
        [MaxLength(200)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string BiggestTrophy { get; set; }
        [MaxLength(10)]
        public double WaterTemp { get; set; }
        [MaxLength(200)]
        public string ImgUrl { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        // Навігаційна властивість: 1 Place -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
