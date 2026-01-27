using CatchTrackerApi.Models;

namespace CatchTrackerApi.Interfaces.ServiceInterfaces
{
    public interface IPlaceService
    {
        Task<List<Place>> GetAllAsync();
        Task<Place?> GetByIdAsync(int id);
        Task<Place> CreateAsync(Place place);
        Task<Place> UpdateAsync(Place place, int id);
        Task<Place?> DeleteAsync(int id);
    }
}
