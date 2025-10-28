using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IRoomAdvertisementRepository : IRepository<RoomAdvertisement>
    {
        Task<IEnumerable<RoomAdvertisement>> GetByRoomIdAsync(Guid roomId);
        Task<IEnumerable<RoomAdvertisement>> GetByAdRequestIdAsync(Guid adRequestId);
        Task<bool> DeleteByAdRequestIdAsync(Guid adRequestId);
    }
}
