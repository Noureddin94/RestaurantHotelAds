using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetAllByUserIdAsync(int userId);
        Task<Restaurant?> GetByIdAsync(int id);
        Task<Restaurant> AddAsync(Restaurant restaurant);
        Task<Restaurant> UpdateAsync(Restaurant restaurant);
    }
}
