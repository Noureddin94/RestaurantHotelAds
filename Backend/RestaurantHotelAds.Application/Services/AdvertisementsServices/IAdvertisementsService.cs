using RestaurantHotelAds.Application.DTOs.AdvertisementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.AdvertisementsServices
{
    public interface IAdvertisementsService
    {
        Task<IEnumerable<AdvertisementDto>> GetAllAdvertisementsAsync(Guid userId);
        Task<AdvertisementDto> GetAdvertisementByIdAsync(Guid advertisementId, Guid userId);
        Task<AdvertisementDto> CreateAdvertisementAsync(CreateAdvertisementDto dto, Guid userId);
        Task<AdvertisementDto?> UpdateAdvertisementAsync(Guid id, UpdateAdvertisementDto dto, Guid userId);
        Task<bool> DeleteAdvertisementAsync(Guid advertisementId, Guid userId);
    }
}
