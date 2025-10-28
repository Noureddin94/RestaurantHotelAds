using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetByHotelIdAsync(Guid hotelId);
        Task<Room?> GetByIdWithHotelAsync(Guid id);
        //Task<Room?> GetByIdAsync(Guid id);
        //Task<Room> AddAsync(Room room);
        //Task<Room> UpdateAsync(Room room);
        //Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsByRoomNumberAsync(Guid hotelId, string roomNumber);
    }
}
