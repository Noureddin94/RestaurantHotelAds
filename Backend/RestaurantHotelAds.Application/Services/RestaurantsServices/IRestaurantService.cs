using RestaurantHotelAds.Application.DTOs.RestaurantDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.RestaurantsServices
{
    public interface IRestaurantService
    {
        Task<RestaurantDto?> GetRestaurantByIdAsync(Guid id, Guid userId);
        Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto dto, Guid userId);
        Task<RestaurantDto?> UpdateRestaurantAsync(Guid id, UpdateRestaurantDto dto, Guid userId);
        Task<bool> DeleteRestaurantAsync(Guid id, Guid userId);
        Task<bool> ToggleRestaurantStatusAsync(Guid id, Guid userId);
    }
}
