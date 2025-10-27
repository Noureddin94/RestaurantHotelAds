using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IAdRequestRepository : IRepository<AdRequest>
    {
        Task<IEnumerable<AdRequest>> GetByHotelIdAsync(Guid hotelId);
        Task<IEnumerable<AdRequest>> GetPendingByHotelIdAsync(Guid hotelId);
        //Task<AdRequest?> GetByIdAsync(Guid id);
        //Task<AdRequest> AddAsync(AdRequest adRequest);
        //Task<AdRequest> UpdateAsync(AdRequest adRequest);
        Task<int> GetPendingCountAsync(Guid hotelId);
    }
}
