using System.Collections.Generic;

namespace CatchTrackerApi.Models
{
    public class FishingLog
    {
        public int Id { get; set; }               // PK
        public int UserId { get; set; }           // FK -> User
        public int PlaceId { get; set; }          // FK -> Place
        public int FishTypeId { get; set; }       // FK -> FishType

        public string Trophy { get; set; }
        public string Bait { get; set; }
        public double Distance { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;

        // Навігаційні властивості
        public User User { get; set; }
        public Place Place { get; set; }
        public FishType FishType { get; set; }
    }
}
