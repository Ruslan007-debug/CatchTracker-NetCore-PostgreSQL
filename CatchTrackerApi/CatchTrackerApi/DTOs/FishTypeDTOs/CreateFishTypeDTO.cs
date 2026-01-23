namespace CatchTrackerApi.DTOs.FishTypeDTOs
{
    public class CreateFishTypeDTO
    {
        public string TypeName { get; set; } = string.Empty;
        public string FavBait { get; set; } = string.Empty;
        public double AvgWeight { get; set; }
    }
}
