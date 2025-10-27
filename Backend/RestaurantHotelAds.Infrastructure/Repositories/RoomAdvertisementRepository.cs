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
    public class RoomAdvertisementRepository : BaseRepository<RoomAdvertisement>, IRoomAdvertisementRepository
    {
        //private readonly ApplicationDbContext _context;

        public RoomAdvertisementRepository(ApplicationDbContext context) : base(context)
        {
            //_context = context;
        }
        public async Task<IEnumerable<RoomAdvertisement>> GetByRoomIdAsync(Guid roomId)
        {
            return await _context.RoomAdvertisements
                .Include(ra => ra.AdRequest)
                .Where(ra => ra.RoomId == roomId && !ra.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomAdvertisement>> GetByAdRequestIdAsync(Guid adRequestId)
        {
            return await _context.RoomAdvertisements
                .Include(ra => ra.Room)
                .Where(ra => ra.AdRequestId == adRequestId && !ra.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> DeleteByAdRequestIdAsync(Guid adRequestId)
        {
            var roomAds = await _context.RoomAdvertisements
                .Where(ra => ra.AdRequestId == adRequestId && !ra.IsDeleted)
                .ToListAsync();

            if (!roomAds.Any()) return false;

            foreach (var roomAd in roomAds)
            {
                roomAd.IsDeleted = true;
                roomAd.DeletedAt = DateTime.UtcNow;
            }
                _context.RoomAdvertisements.UpdateRange(roomAds);
                await _context.SaveChangesAsync();
                return true;
        }

        public Task<IEnumerable<RoomAdvertisement>> GetByAdRequestIdAsync(int adRequestId)
        {
            throw new NotImplementedException();
        }

        //public async Task<IEnumerable<RoomAdvertisement>> GetByRoomIdAsync(Guid roomId)
        //{
        //    var roomAds = await _context.RoomAdvertisements
        //        .Where(ra => ra.RoomId == roomId && !ra.IsDeleted)
        //        .ToListAsync();
        //    return roomAds;
        //}
    }
}
