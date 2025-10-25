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
    public class RoomAdvertisementRepository : IRoomAdvertisementRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomAdvertisementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<RoomAdvertisement> AddAsync(RoomAdvertisement roomAd)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByAdRequestIdAsync(int adRequestId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoomAdvertisement>> GetByAdRequestIdAsync(int adRequestId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoomAdvertisement>> GetByRoomIdAsync(int roomId)
        {
            throw new NotImplementedException();
        }
    }
}
