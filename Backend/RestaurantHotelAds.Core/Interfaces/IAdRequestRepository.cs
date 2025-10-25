using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IAdRequestRepository
    {
        Task<IEnumerable<AdRequest>> GetByHotelIdAsync(int hotelId);
        Task<IEnumerable<AdRequest>> GetPendingByHotelIdAsync(int hotelId);
        Task<AdRequest?> GetByIdAsync(int id);
        Task<AdRequest> AddAsync(AdRequest adRequest);
        Task<AdRequest> UpdateAsync(AdRequest adRequest);
        Task<int> GetPendingCountAsync(int hotelId);
    }
}
