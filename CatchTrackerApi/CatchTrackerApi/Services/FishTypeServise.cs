using CatchTrackerApi.Interfaces.ServiceInterfaces;
using CatchTrackerApi.Interfaces.RepoInterfaces;
using CatchTrackerApi.Models;
using CatchTrackerApi.Queries;

namespace CatchTrackerApi.Services
{
    public class FishTypeServise: IFishTypeService
    {
        private readonly IFishTypeRepository _fishTypeRepo;

        public FishTypeServise(IFishTypeRepository fishTypeRepo)
        {
            _fishTypeRepo = fishTypeRepo;

        }

        public async Task<FishType> CreateAsync(FishType fishTypeModel)
        {
            return await _fishTypeRepo.CreateAsync(fishTypeModel);
        }

        public async Task<FishType?> DeleteAsync(int id)
        {
            var deletedResult = await _fishTypeRepo.DeleteAsync(id);

            if (deletedResult == null)
            {
                throw new KeyNotFoundException($"FishType with ID: {id} is not found");
            }

            return deletedResult;
        }

        public async Task<List<FishType>> GetAllAsync(QueryForFishType query)
        {
            return await _fishTypeRepo.GetAllAsync(query);
        }

        public async Task<FishType?> GetByIdAsync(int id)
        {
            return await _fishTypeRepo.GetByIdAsync(id);
        }

        public async Task<FishType> UpdateAsync(int id, FishType fishTypeModel)
        {
            var updatedResult = await _fishTypeRepo.UpdateAsync(id, fishTypeModel);

            if (updatedResult == null)
            {
                throw new KeyNotFoundException($"FishType with ID: {id} is not found");
            }

            return updatedResult;


        }
    }
}
