using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IAdvertisementRepository
    {
        Task<Advertisement?> GetByIdAsync(int id);
        Task<Advertisement> AddAsync(Advertisement advertisement);
        Task<Advertisement> UpdateAsync(Advertisement advertisement);
    }
}
