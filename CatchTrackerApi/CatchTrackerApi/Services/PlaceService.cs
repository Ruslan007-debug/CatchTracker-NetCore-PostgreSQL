using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Models;

namespace CatchTrackerApi.Services
{
    public class PlaceService: IPlaceService
    {
        public readonly IPlaceRepository _placeRepo;

        public PlaceService(IPlaceRepository placeRepo)
        {
            _placeRepo = placeRepo;
        }

        public async Task<Place> CreateAsync(Place place)
        {
            return await _placeRepo.CreateAsync(place);
        }

        public async Task<Place?> DeleteAsync(int id)
        {
            var deleted = await _placeRepo.DeleteAsync(id);
            if (deleted == null) 
            {
                throw new KeyNotFoundException($"Place with ID: {id} is not found");
            }
            return deleted;
        }

        public async Task<List<Place>> GetAllAsync()
        {
            return await _placeRepo.GetAllAsync();
        }

        public Task<Place?> GetByIdAsync(int id)
        {
            return _placeRepo.GetByIdAsync(id);
        }

        public async Task<Place> UpdateAsync(Place place, int id)
        {
            var updating = await _placeRepo.UpdateAsync(place, id);
            if (updating == null)
            {
                throw new KeyNotFoundException($"Place with ID: {id} is not found");
            }
            return updating;
        }
    }
}
