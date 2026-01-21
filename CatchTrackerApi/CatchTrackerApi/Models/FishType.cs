using System.Collections.Generic;

namespace CatchTrackerApi.Models
{
    public class FishType
    {
        public int Id { get; set; }               // PK
        public string TypeName { get; set; }
        public string FavBait { get; set; }
        public double AvgWeight { get; set; }

        // Навігаційна властивість: 1 FishType -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
