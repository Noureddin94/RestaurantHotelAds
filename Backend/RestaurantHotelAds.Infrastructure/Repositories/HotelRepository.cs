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
    public class HotelRepository : BaseRepository<Hotel>, IHotelRepository
    {
        //private readonly ApplicationDbContext _context;

        public HotelRepository(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }

        /// <summary>
        /// Get all hotels for a specific user
        /// Includes related rooms (eager loading)
        /// </summary>
        public async Task<IEnumerable<Hotel>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)  // Load rooms with hotels (JOIN in SQL)
                .AsNoTracking()  // Improves performance for read-only queries
                .Where(h => !h.IsDeleted && h.UserId == userId)
                .OrderByDescending(h => h.CreatedAt)
                .ToListAsync();  // Execute query and return list
        }

        
        public async Task<Hotel?> GetByIdAndUserIdAsync(Guid id, Guid userId)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId && !h.IsDeleted);
        }
        public async Task<int> DeleteAllByUserIdAsync(Guid userId)
        {
            var hotels = _context.Hotels
                .Where(h => h.UserId == userId && !h.IsDeleted)
                .ToListAsync();

            foreach (var hotel in await hotels)
            {
                hotel.IsDeleted = true; // Soft delete
                hotel.DeletedAt = DateTime.UtcNow;
            }

            //_context.Hotels.RemoveRange(hotels); // Hard delete
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAllAsync()
        {
            var hotels = await _context.Hotels
                .Where(h => !h.IsDeleted)
                .ToListAsync();
            foreach (var hotel in hotels)
            {
                hotel.IsDeleted = true; // Soft delete
                hotel.DeletedAt = DateTime.UtcNow;
            }
            //_context.Hotels.RemoveRange(hotels); // Hard delete
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Get hotel by ID (Excluding deleted ones)
        /// </summary>
        public async Task<Hotel?> GetByIdWithRoomsAsync(Guid id)
        {
            return await _context.Hotels
                .Include(h => h.Rooms)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);  // Returns null if not found
        }

        /// <summary>
        /// Get hotel by ID and verify user owns it
        /// Security check built into the query
        /// </summary>

        /// <summary>
        /// Add new hotel to database
        /// </summary>
        //public async Task<Hotel> AddAsync(Hotel hotel)
        //{
        //    _context.Hotels.Add(hotel);  // Mark as added
        //    await _context.SaveChangesAsync();  // Commit to database
        //    return hotel;  // EF Core updates the Id property
        //}

        /// <summary>
        /// Update existing hotel
        /// </summary>
        //public async Task<Hotel> UpdateAsync(Hotel hotel)
        //{
        //    hotel.UpdatedAt = DateTime.UtcNow;  // Update timestamp
        //    _context.Hotels.Update(hotel);  // Mark as modified
        //    await _context.SaveChangesAsync();
        //    return hotel;
        //}

        /// <summary>
        /// Soft Delete a hotel (mark as deleted instead of removing it)
        /// </summary>
        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var hotel = await _context.Hotels.FindAsync(id);
        //    if (hotel == null || hotel.IsDeleted) return false;

        //    //_context.Hotels.RemoveRange(hotels); // Hard delete
        //    hotel.IsDeleted = true; // Soft delete
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        /// <summary>
        /// Soft Delete All hotels for a specific user
        /// </summery>

        /// <summary>
        /// Check if hotel exists
        /// </summary>
        //public async Task<bool> ExistsAsync(Guid id)
        //{
        //    return await _context.Hotels
        //        .AnyAsync(h => h.Id == id && !h.IsDeleted);
        //}
    }

}
