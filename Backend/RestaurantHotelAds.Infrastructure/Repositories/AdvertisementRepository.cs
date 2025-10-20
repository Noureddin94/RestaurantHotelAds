using Microsoft.EntityFrameworkCore;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Infrastructure.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;

        public AdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Advertisement>> GetByRestaurantIdAsync(int restaurantId)
        {
            return await _context.Advertisements
                .Where(a => a.RestaurantId == restaurantId)
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();
        }

        public async Task<Advertisement?> GetByIdAsync(int id)
        {
            return await _context.Advertisements
                .Include(a => a.Restaurant)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Advertisement> AddAsync(Advertisement advertisement)
        {
            _context.Advertisements.Add(advertisement);
            await _context.SaveChangesAsync();
            return advertisement;
        }

        public async Task<Advertisement> UpdateAsync(Advertisement advertisement)
        {
            _context.Advertisements.Update(advertisement);
            await _context.SaveChangesAsync();
            return advertisement;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ad = await _context.Advertisements.FindAsync(id);
            if (ad == null) return false;

            _context.Advertisements.Remove(ad);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
