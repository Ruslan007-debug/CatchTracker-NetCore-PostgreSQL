using System.Collections.Generic;

namespace CatchTrackerApi.Models
{
    public class Place
    {
        public int Id { get; set; }               // PK
        public string Name { get; set; }
        public string BiggestTrophy { get; set; }
        public double WaterTemp { get; set; }

        // Навігаційна властивість: 1 Place -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
