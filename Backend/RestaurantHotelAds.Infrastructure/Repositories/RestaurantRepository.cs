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
    public class RestaurantRepository : BaseRepository<Restaurant>, IRestaurantRepository
    {
        //private readonly ApplicationDbContext _context;

        public RestaurantRepository(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }

        public async Task<IEnumerable<Restaurant>> GetAllByUserIdAsync(Guid userId)
        {
            return await _context.Restaurants
                .Include(r => r.Advertisements)
                .Where(r => r.UserId == userId && !r.IsDeleted )
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        //public async Task<Restaurant?> GetByIdAsync(Guid id)
        //{
        //    return await _context.Restaurants.FindAsync(id);
        //}

        //public async Task<Restaurant> AddAsync(Restaurant restaurant)
        //{
        //    _context.Restaurants.Add(restaurant);
        //    await _context.SaveChangesAsync();
        //    return restaurant;
        //}

        //public async Task<Restaurant> UpdateAsync(Restaurant restaurant)
        //{
        //    _context.Restaurants.Update(restaurant);
        //    await _context.SaveChangesAsync();
        //    return restaurant;
        //}

        //public async Task<bool> DeleteAsync(Guid id)
        //{
        //    var restaurant = await _context.Restaurants.FindAsync(id);
        //    if (restaurant == null) return false;

        //    restaurant.IsDeleted = true;
        //    restaurant.DeletedAt = DateTime.UtcNow;
            
        //    //_context.Restaurants.Remove(restaurant);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> ExistsAsync(Guid id)
        //{
        //    return await _context.Restaurants.AnyAsync(r => r.Id == id && !r.IsDeleted);
        //}
    }
}
