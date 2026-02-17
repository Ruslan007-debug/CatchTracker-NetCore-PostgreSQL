using CatchTrackerApi.DTOs.FishTypeDTOs;
using CatchTrackerApi.Models;


namespace CatchTrackerApi.Mappers
{
    public static class FishTypeMapper
    {
        public static FishTypeDTO? ToFishTypeDTO(this FishType fishType)
        {
            return new FishTypeDTO
            {
                Id = fishType.Id,
                TypeName = fishType.TypeName,
                FavBait = fishType.FavBait,
                AvgWeight = fishType.AvgWeight,
                Description = fishType.Description,
                ImageUrl = fishType.ImageUrl
            };
        }

        public static FishType ToFishTypeFromCreateDto(this CreateFishTypeDTO createFishType)
        {
            return new FishType
            {
                TypeName = createFishType.TypeName,
                FavBait = createFishType.FavBait,
                AvgWeight = createFishType.AvgWeight,
                Description = createFishType.Description,
                ImageUrl = createFishType.ImageUrl
            };
        }

        public static FishType ToFishTypeFromUpdateDto(this UpdateFishTypeDTO updateFishType)
        {
            return new FishType
            {
                TypeName = updateFishType.TypeName,
                FavBait = updateFishType.FavBait,
                AvgWeight = updateFishType.AvgWeight,
                Description = updateFishType.Description,
                ImageUrl = updateFishType.ImageUrl
            };
        }
    }
}
