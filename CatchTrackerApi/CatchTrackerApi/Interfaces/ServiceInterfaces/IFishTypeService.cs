using CatchTrackerApi.Models;
using CatchTrackerApi.Queries;

namespace CatchTrackerApi.Interfaces.ServiceInterfaces
{
    public interface IFishTypeService
    {
        Task<List<FishType>> GetAllAsync(QueryForFishType query);
        Task<FishType?> GetByIdAsync(int id);
        Task<FishType?> DeleteAsync(int id);
        Task<FishType> CreateAsync(FishType fishTypeModel);
        Task<FishType> UpdateAsync(int id, FishType fishTypeModel);
    }
}
