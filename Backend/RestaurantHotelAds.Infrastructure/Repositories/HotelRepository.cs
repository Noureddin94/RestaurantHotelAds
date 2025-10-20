using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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
    public class HotelRepository : IHotelRepository
    {
        private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all hotels for a specific user
        /// Includes related rooms (eager loading)
        /// </summary>
        public async Task<IEnumerable<Hotel>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)  // Load rooms with hotels (JOIN in SQL)
                .Where(h => h.UserId == userId)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();  // Execute query and return list
        }

        /// <summary>
        /// Get hotel by ID
        /// </summary>
        public async Task<Hotel?> GetByIdAsync(int id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id);  // Returns null if not found
        }

        /// <summary>
        /// Get hotel by ID and verify user owns it
        /// Security check built into the query
        /// </summary>
        public async Task<Hotel?> GetByIdAndUserIdAsync(int id, int userId)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
        }

        /// <summary>
        /// Add new hotel to database
        /// </summary>
        public async Task<Hotel> AddAsync(Hotel hotel)
        {
            _context.Hotels.Add(hotel);  // Mark as added
            await _context.SaveChangesAsync();  // Commit to database
            return hotel;  // EF Core updates the Id property
        }

        /// <summary>
        /// Update existing hotel
        /// </summary>
        public async Task<Hotel> UpdateAsync(Hotel hotel)
        {
            _context.Hotels.Update(hotel);  // Mark as modified
            await _context.SaveChangesAsync();
            return hotel;
        }

        /// <summary>
        /// Delete hotel from database
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null) return false;

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete All hotels for a specific user
        /// </summery>
        
        public async Task<int> DeleteAllByUserIdAsync(int userId)
        {
            var hotels = _context.Hotels.Where(h => h.UserId == userId);
            _context.Hotels.RemoveRange(hotels);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAllAsync()
        {
            var hotels = _context.Hotels;
            _context.Hotels.RemoveRange(hotels);
            await _context.SaveChangesAsync();
            return true;
        }


        /// <summary>
        /// Check if hotel exists
        /// </summary>
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Hotels.AnyAsync(h => h.Id == id);
        }
    }

}
