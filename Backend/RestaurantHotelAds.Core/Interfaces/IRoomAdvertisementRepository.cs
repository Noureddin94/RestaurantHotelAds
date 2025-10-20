using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IRoomAdvertisementRepository
    {
        Task<IEnumerable<RoomAdvertisement>> GetByRoomIdAsync(int roomId);
        Task<IEnumerable<RoomAdvertisement>> GetByAdRequestIdAsync(int adRequestId);
        Task<RoomAdvertisement> AddAsync(RoomAdvertisement roomAd);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteByAdRequestIdAsync(int adRequestId);
    }
}
