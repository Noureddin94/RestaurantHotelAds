using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.BaseServices
{
    public interface IBaseService<TDto>
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<TDto> AddAsync(TDto dto);
        Task<TDto> UpdateAsync(Guid id, TDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
