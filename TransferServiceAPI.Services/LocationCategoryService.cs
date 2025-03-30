using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransferServiceAPI.Models;
using TransferServiceAPI.DataAccess;

namespace TransferServiceAPI.Services
{
    public class LocationCategoryService : ILocationCategoryService
    {
        private readonly TransferServiceDbContext _context;

        public LocationCategoryService(TransferServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<LocationCategory>> GetAllAsync()
        {
            return await _context.LocationCategories
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LocationCategory> GetByIdAsync(int id)
        {
            return await _context.LocationCategories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<LocationCategory> CreateAsync(LocationCategory locationCategory)
        {
            _context.LocationCategories.Add(locationCategory);
            await _context.SaveChangesAsync();
            return locationCategory;
        }

        public async Task<LocationCategory> UpdateAsync(int id, LocationCategory locationCategory)
        {
            var existingCategory = await _context.LocationCategories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = locationCategory.Name;
            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.LocationCategories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return false;
            }

            _context.LocationCategories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}