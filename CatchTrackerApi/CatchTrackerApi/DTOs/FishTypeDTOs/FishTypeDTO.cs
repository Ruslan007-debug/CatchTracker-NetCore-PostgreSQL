namespace CatchTrackerApi.DTOs.FishTypeDTOs
{
    public class FishTypeDTO
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string FavBait { get; set; }= string.Empty;
        public double AvgWeight { get; set; }
    }
}
