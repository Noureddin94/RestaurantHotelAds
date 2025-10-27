using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Application.DTOs.RoomAdvertisementDtos;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.AdRequestServices
{
    public class AdRequestService : IAdRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AdRequestDto>> GetHotelAdRequestsAsync(int hotelId, int userId, string? status = null)
        {
            // Verify hotel ownership
            //var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(hotelId, userId);
            //if (hotel == null) return Enumerable.Empty<AdRequestDto>();

            //var adRequests = status?.ToLower() == "pending"
            //    ? await _unitOfWork.AdRequests.GetPendingByHotelIdAsync(hotelId)
            //    : await _unitOfWork.AdRequests.GetByHotelIdAsync(hotelId);

            //return adRequests.Select(ar => new AdRequestDto
            //{
            //    Id = ar.Id,
            //    AdvertisementId = ar.AdvertisementId,
            //    HotelId = ar.HotelId,
            //    Status = ar.Status,
            //    ReviewNotes = ar.ReviewNotes,
            //    RequestedAt = ar.RequestedAt,
            //    ReviewedAt = ar.ReviewedAt,
            //    AdTitle = ar.Advertisement.Title,
            //    AdDescription = ar.Advertisement.Description,
            //    AdMediaUrl = ar.Advertisement.MediaUrl,
            //    AdMediaType = ar.Advertisement.MediaType,
            //    RestaurantName = ar.Advertisement.Restaurant.Name,
            //    RestaurantAddress = ar.Advertisement.Restaurant.Address,
            //    RestaurantContactPhone = ar.Advertisement.Restaurant.ContactPhone,
            //    AssignedRoomsCount = ar.RoomAdvertisements.Count
            //});
            throw new NotImplementedException();
        }

        public async Task<AdRequestDto?> GetAdRequestByIdAsync(int id, int userId)
        {
            //var adRequest = await _unitOfWork.AdRequests.GetByIdAsync(id);
            //if (adRequest == null) return null;

            //// Verify hotel ownership
            //var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(adRequest.HotelId, userId);
            //if (hotel == null) return null;

            //return new AdRequestDto
            //{
            //    Id = adRequest.Id,
            //    AdvertisementId = adRequest.AdvertisementId,
            //    HotelId = adRequest.HotelId,
            //    Status = adRequest.Status,
            //    ReviewNotes = adRequest.ReviewNotes,
            //    RequestedAt = adRequest.RequestedAt,
            //    ReviewedAt = adRequest.ReviewedAt,
            //    AdTitle = adRequest.Advertisement.Title,
            //    AdDescription = adRequest.Advertisement.Description,
            //    AdMediaUrl = adRequest.Advertisement.MediaUrl,
            //    AdMediaType = adRequest.Advertisement.MediaType,
            //    RestaurantName = adRequest.Advertisement.Restaurant.Name,
            //    RestaurantAddress = adRequest.Advertisement.Restaurant.Address,
            //    RestaurantContactPhone = adRequest.Advertisement.Restaurant.ContactPhone,
            //    AssignedRoomsCount = adRequest.RoomAdvertisements.Count
            //};
            throw new NotImplementedException();
        }

        public async Task<AdRequestDto> ReviewAdRequestAsync(int id, ReviewAdRequestDto dto, int userId)
        {
            //var adRequest = await _unitOfWork.AdRequests.GetByIdAsync(id);
            //if (adRequest == null)
            //    throw new KeyNotFoundException("Ad request not found");

            //// Verify hotel ownership
            //var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(adRequest.HotelId, userId);
            //if (hotel == null)
            //    throw new UnauthorizedAccessException("Access denied");

            //if (adRequest.Status != "Pending")
            //    throw new InvalidOperationException("Only pending requests can be reviewed");

            //if (dto.Status != "Approved" && dto.Status != "Rejected")
            //    throw new ArgumentException("Status must be 'Approved' or 'Rejected'");

            //adRequest.Status = dto.Status;
            //adRequest.ReviewNotes = dto.ReviewNotes;
            //adRequest.ReviewedAt = DateTime.UtcNow;
            //adRequest.ReviewedBy = userId;

            //await _unitOfWork.AdRequests.UpdateAsync(adRequest);
            //await _unitOfWork.SaveChangesAsync();

            //return new AdRequestDto
            //{
            //    Id = adRequest.Id,
            //    AdvertisementId = adRequest.AdvertisementId,
            //    HotelId = adRequest.HotelId,
            //    Status = adRequest.Status,
            //    ReviewNotes = adRequest.ReviewNotes,
            //    RequestedAt = adRequest.RequestedAt,
            //    ReviewedAt = adRequest.ReviewedAt,
            //    AdTitle = adRequest.Advertisement.Title,
            //    AdDescription = adRequest.Advertisement.Description,
            //    AdMediaUrl = adRequest.Advertisement.MediaUrl,
            //    AdMediaType = adRequest.Advertisement.MediaType,
            //    RestaurantName = adRequest.Advertisement.Restaurant.Name,
            //    RestaurantAddress = adRequest.Advertisement.Restaurant.Address,
            //    RestaurantContactPhone = adRequest.Advertisement.Restaurant.ContactPhone,
            //    AssignedRoomsCount = adRequest.RoomAdvertisements.Count
            //};
            throw new NotImplementedException();
        }

        public async Task<bool> AssignAdToRoomsAsync(AssignAdToRoomsDto dto, int userId)
        {
            //var adRequest = await _unitOfWork.AdRequests.GetByIdAsync(dto.AdRequestId);
            //if (adRequest == null)
            //    throw new KeyNotFoundException("Ad request not found");

            //// Verify hotel ownership
            //var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(adRequest.HotelId, userId);
            //if (hotel == null)
            //    throw new UnauthorizedAccessException("Access denied");

            //if (adRequest.Status != "Approved")
            //    throw new InvalidOperationException("Only approved ads can be assigned to rooms");

            //await _unitOfWork.BeginTransactionAsync();

            //try
            //{
            //    // Remove existing assignments for this ad request
            //    await _unitOfWork.RoomAdvertisements.DeleteByAdRequestIdAsync(dto.AdRequestId);

            //    // Add new assignments
            //    foreach (var roomId in dto.RoomIds)
            //    {
            //        // Verify room belongs to the hotel
            //        var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            //        if (room == null || room.HotelId != adRequest.HotelId)
            //            continue;

            //        var roomAd = new RoomAdvertisement
            //        {
            //            AdRequestId = dto.AdRequestId,
            //            RoomId = roomId,
            //            DisplayOrder = dto.DisplayOrder,
            //            DurationSeconds = dto.DurationSeconds,
            //            IsActive = true,
            //            AssignedAt = DateTime.UtcNow
            //        };

            //        await _unitOfWork.RoomAdvertisements.AddAsync(roomAd);
            //    }

            //    await _unitOfWork.SaveChangesAsync();
            //    await _unitOfWork.CommitTransactionAsync();
            //    return true;
            //}
            //catch
            //{
            //    await _unitOfWork.RollbackTransactionAsync();
            //    throw;
            //}
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoomAdvertisementDto>> GetRoomAdvertisementsAsync(int roomId, int userId)
        {
            //var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
            //if (room == null) return Enumerable.Empty<RoomAdvertisementDto>();

            //// Verify hotel ownership
            //var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(room.HotelId, userId);
            //if (hotel == null) return Enumerable.Empty<RoomAdvertisementDto>();

            //var roomAds = await _unitOfWork.RoomAdvertisements.GetByRoomIdAsync(roomId);

            //return roomAds.Select(ra => new RoomAdvertisementDto
            //{
            //    Id = ra.Id,
            //    RoomId = ra.RoomId,
            //    RoomNumber = ra.Room.RoomNumber,
            //    AdTitle = ra.AdRequest.Advertisement.Title,
            //    AdMediaUrl = ra.AdRequest.Advertisement.MediaUrl,
            //    DisplayOrder = ra.DisplayOrder,
            //    DurationSeconds = ra.DurationSeconds,
            //    IsActive = ra.IsActive,
            //    AssignedAt = ra.AssignedAt
            //});
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAdFromRoomAsync(int roomAdvertisementId, int userId)
        {
            //var roomAd = await _unitOfWork.RoomAdvertisements.GetByRoomIdAsync(roomAdvertisementId);
            //if (!roomAd.Any()) return false;

            //var firstRoomAd = roomAd.First();
            //var room = await _unitOfWork.Rooms.GetByIdAsync(firstRoomAd.RoomId);
            //if (room == null) return false;

            //// Verify hotel ownership
            //var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(room.HotelId, userId);
            //if (hotel == null) return false;

            //await _unitOfWork.RoomAdvertisements.DeleteAsync(roomAdvertisementId);
            //await _unitOfWork.SaveChangesAsync();
            //return true;
            throw new NotImplementedException();
        }
    }
}
