using CatchTrackerApi.Models;

namespace CatchTrackerApi.Interfaces.RepoInterfaces
{
    public interface IFishingLogRepository
    {
        Task<List<FishingLog>> GetAllFishingLogsAsync();
        Task<FishingLog?> GetFishingLogByIdAsync(int id);
        Task<List<FishingLog>> GetFishingLogsByUserIdAsync(int userId);
        Task<FishingLog> CreateFishingLogAsync(FishingLog fishingLog);
        Task<FishingLog?> UpdateFishingLogAsync(FishingLog fishingLog, int id);
        Task<FishingLog> DeleteFishingLogAsync(int id);
        Task<List<FishingLog>> GetLeaderboardAsync(int limit);
    }
}
