using System.Collections.Generic;
using System.Threading.Tasks;
using TransferServiceAPI.Models;

namespace TransferServiceAPI.Services
{
    public interface ILocationService
    {
        Task<List<Location>> GetAllAsync();
        Task<Location> GetByIdAsync(int id);
        Task<Location> CreateAsync(Location location);
        Task<Location> UpdateAsync(int id, Location location);
        Task<bool> DeleteAsync(int id);
    }
}