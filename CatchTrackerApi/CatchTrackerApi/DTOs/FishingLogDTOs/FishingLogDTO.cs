using CatchTrackerApi.DTOs.PlaceDTOs;
using CatchTrackerApi.DTOs.FishTypeDTOs;

namespace CatchTrackerApi.DTOs.FishingLogDTOs
{
    public class FishingLogDTO
    {
        public int Id { get; set; }               
        public int UserId { get; set; }           
        public PlaceDTO Place { get; set; }          
        public FishTypeDTO FishType { get; set; }       

        public string Trophy { get; set; }
        public double Weight { get; set; }
        public string Bait { get; set; }
        public double Distance { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
