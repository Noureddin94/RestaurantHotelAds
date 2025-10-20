using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllByUserIdAsync(int userId);
        Task<Hotel?> GetByIdAsync(int id);
        Task<Hotel?> GetByIdAndUserIdAsync(int id, int userId);
        Task<Hotel> AddAsync(Hotel hotel);
        Task<Hotel> UpdateAsync(Hotel hotel);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
