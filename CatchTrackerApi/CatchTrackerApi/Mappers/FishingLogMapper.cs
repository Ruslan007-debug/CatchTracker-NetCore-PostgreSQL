using CatchTrackerApi.DTOs.FishingLogDTOs;
using CatchTrackerApi.Models;

namespace CatchTrackerApi.Mappers
{
    public static class FishingLogMapper
    {
        public static FishingLogDTO ToFishingLogDTO(this FishingLog fishingLog)
        {
            return new FishingLogDTO
            {
                Id = fishingLog.Id,
                UserId = fishingLog.UserId,
                Place = fishingLog.Place?.ToPlaceDTO(),
                FishType = fishingLog.FishType?.ToFishTypeDTO(),
                Trophy = fishingLog.Trophy,
                Bait = fishingLog.Bait,
                Distance = fishingLog.Distance,
                Time = fishingLog.Time
            };
        }

        public static FishingLog ToFishingLogFromCreateDTO(this CreateFishingLogDTO fishingLogCreateDTO, int userId)
        {
            return new FishingLog
            {
                UserId = userId,
                PlaceId = fishingLogCreateDTO.PlaceId,
                FishTypeId = fishingLogCreateDTO.FishTypeId,
                Trophy = fishingLogCreateDTO.Trophy,
                Bait = fishingLogCreateDTO.Bait,
                Distance = fishingLogCreateDTO.Distance
            };
        }

        public static FishingLog ToFishingLogFromUpdateDTO(this UpdateFishingLogDTO updatingModel)
        {
            return new FishingLog
            {
                PlaceId = updatingModel.PlaceId,
                FishTypeId = updatingModel.FishTypeId,
                Trophy = updatingModel.Trophy,
                Bait = updatingModel.Bait,
                Distance = updatingModel.Distance
            };
        }
    }
}
