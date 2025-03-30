using System.Collections.Generic;
using System.Threading.Tasks;
using TransferServiceAPI.Models;

namespace TransferServiceAPI.Services
{
    public interface ILocationCategoryService
    {
        Task<List<LocationCategory>> GetAllAsync();
        Task<LocationCategory> GetByIdAsync(int id);
        Task<LocationCategory> CreateAsync(LocationCategory locationCategory);
        Task<LocationCategory> UpdateAsync(int id, LocationCategory locationCategory);
        Task<bool> DeleteAsync(int id);
    }
}