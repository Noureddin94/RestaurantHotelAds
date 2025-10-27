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
        //Task<IEnumerable<Advertisement>> GetAllAsync();
        Task<IEnumerable<Advertisement>> GetByRestaurantIdAsync(Guid restaurantId);
        Task<Advertisement?> GetByIdWithRestaurantAsync(Guid id);
        //Task<Advertisement> AddAsync(Advertisement advertisement);
        //Task<Advertisement> UpdateAsync(Advertisement advertisement);
        //Task<bool> DeleteAsync(Guid id);
    }
}
