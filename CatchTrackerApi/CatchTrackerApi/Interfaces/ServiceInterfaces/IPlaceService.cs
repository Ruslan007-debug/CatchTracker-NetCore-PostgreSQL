using CatchTrackerApi.Models;
using CatchTrackerApi.Queries;

namespace CatchTrackerApi.Interfaces.ServiceInterfaces
{
    public interface IPlaceService
    {
        Task<List<Place>> GetAllAsync(QueryForPlace query);
        Task<Place?> GetByIdAsync(int id);
        Task<Place> CreateAsync(Place place);
        Task<Place> UpdateAsync(Place place, int id);
        Task<Place?> DeleteAsync(int id);
    }
}
