using CatchTrackerApi.DTOs.PlaceDTOs;
using CatchTrackerApi.Models;

namespace CatchTrackerApi.Mappers
{
    public static class PlaceMapper
    {
        public static PlaceDTO ToPlaceDTO(this Place place)
        {
            return new PlaceDTO
            {
                Id = place.Id,
                Name = place.Name,
                BiggestTrophy = place.BiggestTrophy,
                WaterTemp = place.WaterTemp
            };
        }

        public static Place ToPlaceFromCreateDTO(this CreatePlaceDTO create)
        {
            return new Place
            {
                Name = create.Name,
                BiggestTrophy = create.BiggestTrophy,
                WaterTemp = create.WaterTemp
            };

        }

        public static Place ToPlaceFromUpdateDTO(this UpdatePlaceDTO update)
        {
            return new Place
            {
                Name = update.Name,
                BiggestTrophy = update.BiggestTrophy,
                WaterTemp = update.WaterTemp
            };
        }
    }
}
