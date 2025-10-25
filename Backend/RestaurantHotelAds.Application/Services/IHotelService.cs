using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services
{
    public interface IHotelService
    {
        // Hotel Operations
        Task<IEnumerable<HotelDto>> GetAllHotelsAsync(int userId);
        Task<HotelDto?> GetHotelByIdAsync(int id, int userId);
        Task<HotelDto> CreateHotelAsync(CreateHotelDto dto, int userId);
        Task<HotelDto?> UpdateHotelAsync(int id, UpdateHotelDto dto, int userId);
        Task<bool> DeleteHotelAsync(int id, int userId);

        // Room Operations
        Task<IEnumerable<RoomDto>> GetHotelRoomsAsync(int hotelId, int userId);
        Task<RoomDto> CreateRoomAsync(int hotelId, CreateRoomDto dto, int userId);
        Task<bool> DeleteRoomAsync(int roomId, int userId);
    }

    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HotelService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<HotelDto> CreateHotelAsync(CreateHotelDto dto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<RoomDto> CreateRoomAsync(int hotelId, CreateRoomDto dto, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHotelAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRoomAsync(int roomId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync(int userId)
        {
            var hotels = await _unitOfWork.Hotels.GetAllByUserIdAsync(userId);
            var hotelDtos = new List<HotelDto>();

            foreach (var hotel in hotels)
            {
                // Get pending ads count (currently returns 0 from dummy repository)
                var pendingCount = await _unitOfWork.AdRequests.GetPendingCountAsync(hotel.Id);

                hotelDtos.Add(new HotelDto
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    Description = hotel.Description,
                    Address = hotel.Address,
                    ContactPhone = hotel.ContactPhone,
                    ContactEmail = hotel.ContactEmail,
                    TotalRooms = hotel.TotalRooms,
                    IsActive = hotel.IsActive,
                    CreatedAt = hotel.CreatedAt,
                    RoomsCount = hotel.Rooms?.Count ?? 0,
                    PendingAdsCount = pendingCount
                });
            }

            return hotelDtos;
        }

        public async Task<HotelDto?> GetHotelByIdAsync(int id, int userId)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(id, userId);
            if (hotel == null) return null;

            var pendingCount = await _unitOfWork.AdRequests.GetPendingCountAsync(hotel.Id);

            return new HotelDto
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Description = hotel.Description,
                Address = hotel.Address,
                ContactPhone = hotel.ContactPhone,
                ContactEmail = hotel.ContactEmail,
                TotalRooms = hotel.TotalRooms,
                IsActive = hotel.IsActive,
                CreatedAt = hotel.CreatedAt,
                RoomsCount = hotel.Rooms?.Count ?? 0,
                PendingAdsCount = pendingCount
            };
        }

        public Task<IEnumerable<RoomDto>> GetHotelRoomsAsync(int hotelId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<HotelDto?> UpdateHotelAsync(int id, UpdateHotelDto dto, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
