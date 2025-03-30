using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransferServiceAPI.DataAccess;
using TransferServiceAPI.Models;

namespace TransferServiceAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly TransferServiceDbContext _context;

        public LocationService(TransferServiceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetAllAsync()
        {
            return await _context.Locations
                .Include(l => l.LocationCategory)
                .ToListAsync();
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _context.Locations
                .Include(l => l.LocationCategory)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Location> CreateAsync(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task<Location> UpdateAsync(int id, Location location)
        {
            var existingLocation = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == id);
            if (existingLocation == null) return null;

            existingLocation.Name = location.Name;
            existingLocation.LocationCategoryId = location.LocationCategoryId;
            await _context.SaveChangesAsync();
            return existingLocation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == id);
            if (location == null) return false;

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}