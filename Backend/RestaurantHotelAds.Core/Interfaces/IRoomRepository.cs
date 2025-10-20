using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetByHotelIdAsync(int hotelId);
        Task<Room?> GetByIdAsync(int id);
        Task<Room> AddAsync(Room room);
        Task<Room> UpdateAsync(Room room);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsByRoomNumberAsync(int hotelId, string roomNumber);
    }
}
