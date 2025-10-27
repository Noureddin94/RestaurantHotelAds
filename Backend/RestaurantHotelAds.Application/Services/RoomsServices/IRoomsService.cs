using RestaurantHotelAds.Application.DTOs;
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
        Task<IEnumerable<RoomDto>> GetHotelRoomsAsync(Guid hotelId, Guid userId);
        Task<RoomDto> CreateRoomAsync(Guid hotelId, CreateRoomAdvertisementDto dto, Guid userId);
        Task<bool> DeleteRoomAsync(Guid roomId, Guid userId);
    }
}
