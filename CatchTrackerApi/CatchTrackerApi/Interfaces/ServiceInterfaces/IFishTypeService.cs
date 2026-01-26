using CatchTrackerApi.Models;

namespace CatchTrackerApi.Interfaces.ServiceInterfaces
{
    public interface IFishTypeService
    {
        Task<List<FishType>> GetAllAsync();
        Task<FishType?> GetByIdAsync(int id);
        Task<FishType?> DeleteAsync(int id);
        Task<FishType> CreateAsync(FishType fishTypeModel);
        Task<FishType> UpdateAsync(int id, FishType fishTypeModel);
    }
}
