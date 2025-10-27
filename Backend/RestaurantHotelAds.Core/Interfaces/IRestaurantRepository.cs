using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetAllByUserIdAsync(Guid userUuid);
        //Task<Restaurant?> GetByIdAsync(Guid id);
        //Task<Restaurant> AddAsync(Restaurant restaurant);
        //Task<Restaurant> UpdateAsync(Restaurant restaurant);
        //Task<bool> DeleteAsync(Guid id);
        //Task<bool> ExistsAsync(Guid id);
    }
}
