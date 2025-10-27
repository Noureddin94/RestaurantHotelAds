using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IHotelRepository : IRepository<Hotel>
    {
        Task<IEnumerable<Hotel>> GetAllByUserIdAsync(Guid userUuid);
        Task<Hotel?> GetByIdWithRoomsAsync(Guid id);
        Task<Hotel?> GetByIdAndUserIdAsync(Guid id, Guid userUuid);
        Task<int> DeleteAllByUserIdAsync(Guid userUuid);
        Task<bool> DeleteAllAsync();

        //Task<Hotel?> GetByIdAsync(Guid id);
        //Task<Hotel> AddAsync(Hotel hotel);
        //Task<Hotel> UpdateAsync(Hotel hotel);
        //Task<bool> DeleteAsync(Guid id);
        //Task<bool> ExistsAsync(Guid id);
    }
}
