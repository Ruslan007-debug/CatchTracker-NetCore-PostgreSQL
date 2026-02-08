using CatchTrackerApi.Data;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Models;
using CatchTrackerApi.Queries;
using Microsoft.EntityFrameworkCore;

namespace CatchTrackerApi.Repos
{
    public class PlaceRepository : IPlaceRepository
    {
        private readonly FishingDbContext _database;

        public PlaceRepository(FishingDbContext database)
        {
            _database = database;
        }

        public async Task<Place> CreateAsync(Place place)
        {
            await _database.Places.AddAsync(place);
            await _database.SaveChangesAsync();
            return place;
        }

        public async Task<Place?> DeleteAsync(int id)
        {
            var place = await _database.Places.FindAsync(id);
            if (place == null)
            {
                return null;
            }
            _database.Places.Remove(place);
            await _database.SaveChangesAsync();
            return place;
        }

        public async Task<List<Place>> GetAllAsync(QueryForPlace query)
        {
            var place = _database.Places.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                place = place.Where(p => EF.Functions.ILike(p.Name, $"%{query.Name}%"));
            }
            return await place.OrderBy(f=>f.Id).ToListAsync();
        }

        public async Task<Place?> GetByIdAsync(int id)
        {
            return await _database.Places.FindAsync(id);
        }

        public async Task<Place> UpdateAsync(Place place, int id)
        {
            var updatingPlace = await _database.Places.FindAsync(id);
            if (updatingPlace == null)
            {
                return null;
            }
            updatingPlace.Name = place.Name;
            updatingPlace.BiggestTrophy = place.BiggestTrophy;
            updatingPlace.WaterTemp = place.WaterTemp;

            await _database.SaveChangesAsync();
            return updatingPlace;
        }
    }
}
