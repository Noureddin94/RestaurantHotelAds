using Microsoft.EntityFrameworkCore;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Infrastructure.Repositories
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
        {
            // NOTE: In real apps you should prefer UserManager<ApplicationUser> to create users
            // because it handles password hashing, normalization, claims, roles, etc.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> UpdateAsync(ApplicationUser user)
        {
            user.LastLoginAt = user.LastLoginAt ?? user.LastLoginAt; // preserve if set outside
            user.CreatedAt = user.CreatedAt; // keep created at unchanged
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<ApplicationUser> DeleteAsync(ApplicationUser user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<bool> ExistsByUuidAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

    }
}
