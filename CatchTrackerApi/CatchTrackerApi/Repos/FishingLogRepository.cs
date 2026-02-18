using CatchTrackerApi.Data;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatchTrackerApi.Repos
{
    public class FishingLogRepository: IFishingLogRepository
    {
        private readonly FishingDbContext _database;

        public FishingLogRepository(FishingDbContext database)
        {
            _database = database;
        }

        public async Task<FishingLog> CreateFishingLogAsync(FishingLog fishingLog)
        {
            await _database.FishingLogs.AddAsync(fishingLog);
            await _database.SaveChangesAsync();
            return fishingLog;
        }

        public async Task<FishingLog> DeleteFishingLogAsync(int id)
        {
            var deleting =  await _database.FishingLogs.FindAsync(id);
            if (deleting == null)
            {
                return null;
            }
            _database.FishingLogs.Remove(deleting);
            await _database.SaveChangesAsync();
            return deleting;
        }

        public async Task<List<FishingLog>> GetAllFishingLogsAsync()
        {
            return await _database.FishingLogs
                .Include(f => f.Place)
                .Include(f=>f.FishType)
                .ToListAsync();
        }

        public async Task<FishingLog?> GetFishingLogByIdAsync(int id)
        {
            return await _database.FishingLogs
                .Include(f => f.Place)
                .Include(f => f.FishType)
                .FirstOrDefaultAsync(f=>f.Id==id);
        }

        public async Task<List<FishingLog>> GetFishingLogsByUserIdAsync(int userId)
        {
            return await _database.FishingLogs
                .Include(f => f.Place)
                .Include(f => f.FishType)
                .Where(f=>f.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<FishingLog>> GetLeaderboardAsync(int limit)
        {
            return await _database.FishingLogs
               .Include(l => l.User)
               .Include(l => l.FishType)
               .Include(l => l.Place)
               .OrderByDescending(l => l.Weight)
               .Take(limit)
               .ToListAsync();
        }

        public async Task<FishingLog?> UpdateFishingLogAsync(FishingLog fishingLog, int id)
        {
            var updating = await _database.FishingLogs.FindAsync(id);
            if (updating == null)
            {
                return null;
            }
            updating.PlaceId = fishingLog.PlaceId;
            updating.FishTypeId = fishingLog.FishTypeId;
            updating.Trophy = fishingLog.Trophy;
            updating.Bait = fishingLog.Bait;
            updating.Distance = fishingLog.Distance;

            await _database.SaveChangesAsync();             //у сервісі після оновлення логу потрібно
                                                            //створити нову змінну та впихнути туди модель знайдену за
                                                            //допомогою гетБайАйді щоб поля плейс та фішТайп були заповнені
            return updating;
        }
    }
}
