using CatchTrackerApi.DTOs.FishTypeDTOs;
using CatchTrackerApi.DTOs.PlaceDTOs;

namespace CatchTrackerApi.DTOs.FishingLogDTOs
{
    public class CreateFishingLogDTO
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public int FishTypeId { get; set; }

        public string Trophy { get; set; }
        public string Bait { get; set; }
        public double Distance { get; set; }
    }
}
