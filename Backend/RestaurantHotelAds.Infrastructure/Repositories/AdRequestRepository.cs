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
    public class AdRequestRepository : IAdRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public AdRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AdRequest> AddAsync(AdRequest adRequest)
        {
            _context.AdRequests.Add(adRequest);
            await _context.SaveChangesAsync();
            return adRequest;
        }

        public async Task<IEnumerable<AdRequest>> GetByHotelIdAsync(int hotelId)
        {
            return await _context.AdRequests
                .Where(ar => ar.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<AdRequest?> GetByIdAsync(int id)
        {
            return await _context.AdRequests
                .FirstOrDefaultAsync(ar => ar.Id == id);
        }

        public async Task<IEnumerable<AdRequest>> GetPendingByHotelIdAsync(int hotelId)
        {
            return await _context.AdRequests
                .Where(ar => ar.HotelId == hotelId)
                .ToListAsync();
        }

        public async Task<int> GetPendingCountAsync(int hotelId)
        {
            return await _context.AdRequests
                .Where(ar => ar.HotelId == hotelId && ar.Status == "Pending")
                .CountAsync();
        }

        public Task<AdRequest> UpdateAsync(AdRequest adRequest)
        {
            _context.AdRequests.Update(adRequest);
            return Task.FromResult(adRequest);
        }
    }
}
