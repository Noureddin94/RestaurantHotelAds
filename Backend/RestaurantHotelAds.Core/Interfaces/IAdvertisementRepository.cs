using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IAdvertisementRepository : IRepository<Advertisement>
    {
        Task<IEnumerable<Advertisement>> GetByRestaurantIdAsync(Guid restaurantId);
        Task<Advertisement?> GetByIdWithRestaurantAsync(Guid id);
        Task<IEnumerable<Advertisement>> GetByUserIdAsync(Guid userId);
        // For restaurant owners to see their ads
        Task<IEnumerable<Advertisement>> GetByRestaurantOwnerIdAsync(Guid restaurantOwnerId);
    }
}
