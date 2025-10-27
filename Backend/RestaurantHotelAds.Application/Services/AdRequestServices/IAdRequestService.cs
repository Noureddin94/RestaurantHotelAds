using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Application.DTOs.RoomAdvertisementDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.AdRequestServices
{
    public interface IAdRequestService
    {
        Task<IEnumerable<AdRequestDto>> GetHotelAdRequestsAsync(int hotelId, int userId, string? status = null);
        Task<AdRequestDto?> GetAdRequestByIdAsync(int id, int userId);
        Task<AdRequestDto> ReviewAdRequestAsync(int id, ReviewAdRequestDto dto, int userId);
        Task<bool> AssignAdToRoomsAsync(AssignAdToRoomsDto dto, int userId);
        Task<IEnumerable<RoomAdvertisementDto>> GetRoomAdvertisementsAsync(int roomId, int userId);
        Task<bool> RemoveAdFromRoomAsync(int roomAdvertisementId, int userId);
    }
}
