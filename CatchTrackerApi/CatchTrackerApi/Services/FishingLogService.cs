using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Mappers;
using CatchTrackerApi.Models;

namespace CatchTrackerApi.Services
{
    public class FishingLogService: IFishingLogService
    {
        private readonly IFishingLogRepository _repo;

        public FishingLogService(IFishingLogRepository repo)
        {
            _repo = repo;
        }

        public async Task<FishingLog> CreateFishingLogAsync(FishingLog fishingLog)
        {
            var createdFishingLog = await _repo.CreateFishingLogAsync(fishingLog);

            var fullCreatedFishingLog = await _repo.GetFishingLogByIdAsync(createdFishingLog.Id);

            return fullCreatedFishingLog;
        }

        public async Task<FishingLog> DeleteFishingLogAsync(int id)
        {
            var deleted = await _repo.DeleteFishingLogAsync(id);
            if (deleted == null)
            {
                throw new KeyNotFoundException($"Fishing log with id {id} not found.");
            }
            return deleted;
        }

        public async Task<List<FishingLog>> GetAllFishingLogsAsync()
        {
            return await _repo.GetAllFishingLogsAsync();
        }

        public async Task<FishingLog?> GetFishingLogByIdAsync(int id)
        {
            return await _repo.GetFishingLogByIdAsync(id);
        }

        public async Task<List<FishingLog>> GetFishingLogsByUserIdAsync(int userId)
        {
            return await _repo.GetFishingLogsByUserIdAsync(userId);
        }

        public async Task<List<FishingLog>> GetLeaderboardAsync(int limit)
        {
            if (limit <= 0) limit = 50;
            if (limit > 100) limit = 100;

            return await _repo.GetLeaderboardAsync(limit);
        }

        public async Task<FishingLog?> UpdateFishingLogAsync(FishingLog fishingLog, int id)
        {
            var updated = await _repo.UpdateFishingLogAsync(fishingLog, id);
            if (updated == null)
            {
                throw new KeyNotFoundException($"Fishing log with id {id} not found.");
            }
            var fullUpdated = await _repo.GetFishingLogByIdAsync(id);
            return fullUpdated;
        }
    }
}
