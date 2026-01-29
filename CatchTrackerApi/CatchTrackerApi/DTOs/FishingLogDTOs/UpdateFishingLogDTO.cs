namespace CatchTrackerApi.DTOs.FishingLogDTOs
{
    public class UpdateFishingLogDTO
    {
        public int PlaceId { get; set; }
        public int FishTypeId { get; set; }

        public string Trophy { get; set; }
        public string Bait { get; set; }
        public double Distance { get; set; }
    }
}
