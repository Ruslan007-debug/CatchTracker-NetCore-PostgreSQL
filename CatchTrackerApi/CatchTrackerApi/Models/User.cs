using System.Collections.Generic;

namespace CatchTrackerApi.Models
{
    public class User
    {
        public int Id { get; set; }               // PK
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Навігаційна властивість: 1 User -> M FishingLogs
        public ICollection<FishingLog> FishingLogs { get; set; }
    }
}
