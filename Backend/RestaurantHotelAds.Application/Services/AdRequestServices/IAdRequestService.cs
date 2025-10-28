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
        Task<IEnumerable<AdRequestDto>> GetHotelAdRequestsAsync(Guid hotelId, Guid userId, string? status = null);
        Task<AdRequestDto?> GetAdRequestByIdAsync(Guid id, Guid userId);
        Task<AdRequestDto> ReviewAdRequestAsync(Guid id, ReviewAdRequestDto dto, Guid userId);
        Task<bool> AssignAdToRoomsAsync(AssignAdToRoomsDto dto, Guid userId);
        Task<IEnumerable<RoomAdvertisementDto>> GetRoomAdvertisementsAsync(Guid roomId, Guid userId);
        Task<bool> RemoveAdFromRoomAsync(Guid roomAdvertisementId, Guid userId);
    }
}
