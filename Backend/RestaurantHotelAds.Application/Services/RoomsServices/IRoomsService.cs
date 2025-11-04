using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Application.DTOs.HotelDtos;
using RestaurantHotelAds.Application.DTOs.RoomAdvertisementDtos;
using RestaurantHotelAds.Application.DTOs.RoomDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.RoomsServices
{
    public interface IRoomsService
    {
        // Room Operations
        Task<IEnumerable<RoomDto>> GetAllHotelRoomsAsync(Guid hotelId, Guid userId);
        Task<IEnumerable<RoomDto>> GetHotelRoomsAsync(Guid hotelId);
        Task<RoomDto> GetRoomByIdAsync(Guid roomId, Guid userId);
        Task<RoomDto> CreateRoomAsync(Guid hotelId, CreateRoomAdvertisementDto dto, Guid userId);
        Task<RoomDto?> UpdateHotelRoomsAsync(Guid id, UpdateRoomDto dto, Guid userId);
        Task<bool> DeleteRoomAsync(Guid roomId, Guid userId);
    }
}
