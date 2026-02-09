using CatchTrackerApi.Data;
using CatchTrackerApi.Models;
using Microsoft.EntityFrameworkCore;
using CatchTrackerApi.Interfaces.RepoInterfaces;

namespace CatchTrackerApi.Repos
{
    public class FishTypeRepository: IFishTypeRepository
    {
        private readonly FishingDbContext _database;

        public FishTypeRepository (FishingDbContext database)
        {
            _database = database;
        }

        public async Task<FishType> CreateAsync(FishType fishTypeModel)
        {
            await _database.FishTypes.AddAsync(fishTypeModel);
            await _database.SaveChangesAsync();
            return fishTypeModel;
        }

        public async Task<FishType?> DeleteAsync(int id)
        {
            var deletingType = await _database.FishTypes.FindAsync(id);
            if (deletingType == null)
            {
                return null;
            }
            _database.FishTypes.Remove(deletingType);
            await _database.SaveChangesAsync();
            return deletingType;
        }

        public async Task<List<FishType>> GetAllAsync()
        {
            return await _database.FishTypes
                .AsNoTracking()
                .OrderBy(f=>f.Id)
                .ToListAsync();
        }

        public async Task<FishType?> GetByIdAsync(int id)
        {
            return await _database.FishTypes.FindAsync(id);
        }

        public async Task<FishType> UpdateAsync(int id, FishType fishTypeModel)
        {
            var existing = await _database.FishTypes.FindAsync(id);
            if (existing == null)
            {
                return null;
            }

            existing.TypeName = fishTypeModel.TypeName;
            existing.AvgWeight = fishTypeModel.AvgWeight;
            existing.FavBait = fishTypeModel.FavBait;

            await _database.SaveChangesAsync();
            return existing; 
        }
    }
}
