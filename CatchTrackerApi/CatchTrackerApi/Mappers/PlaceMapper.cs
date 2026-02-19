using CatchTrackerApi.DTOs.PlaceDTOs;
using CatchTrackerApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;

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
                WaterTemp = place.WaterTemp,
                ImgUrl = place.ImgUrl,
                Description = place.Description,
                PondId = place.PondId
            };
        }

        public static Place ToPlaceFromCreateDTO(this CreatePlaceDTO create)
        {
            return new Place
            {
                Name = create.Name,
                BiggestTrophy = create.BiggestTrophy,
                WaterTemp = create.WaterTemp,
                ImgUrl = create.ImgUrl,
                Description = create.Description,
                PondId = create.PondId
            };

        }

        public static Place ToPlaceFromUpdateDTO(this UpdatePlaceDTO update)
        {
            return new Place
            {
                Name = update.Name,
                BiggestTrophy = update.BiggestTrophy,
                WaterTemp = update.WaterTemp,
                ImgUrl = update.ImgUrl,
                Description = update.Description,
                PondId = update.PondId
            };
        }
    }
}
