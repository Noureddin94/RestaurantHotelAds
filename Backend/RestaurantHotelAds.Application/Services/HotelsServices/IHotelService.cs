using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Application.DTOs.HotelDtos;
using RestaurantHotelAds.Application.DTOs.RoomAdvertisementDtos;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.HotelsServices
{
    public interface IHotelService
    {
        // Hotel Operations
        Task<IEnumerable<HotelDto>> GetAllHotelsAsync(Guid userId);
        Task<HotelDto?> GetHotelByIdAsync(Guid id, Guid userId);
        Task<HotelDto> CreateHotelAsync(CreateHotelDto dto, Guid userId);
        Task<HotelDto?> UpdateHotelAsync(Guid id, UpdateHotelDto dto, Guid userId);
        Task<bool> DeleteHotelAsync(Guid id, Guid userId);
    }
}
