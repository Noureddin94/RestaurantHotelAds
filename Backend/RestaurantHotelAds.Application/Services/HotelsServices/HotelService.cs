using AutoMapper;
using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Application.DTOs.HotelDtos;
using RestaurantHotelAds.Application.DTOs.RoomAdvertisementDtos;
using RestaurantHotelAds.Application.Services.BaseServices;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using RestaurantHotelAds.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.HotelsServices
{
    public class HotelService :  IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync(Guid userId)
        {
            var hotels = await _unitOfWork.Hotels.GetAllByUserIdAsync(userId);
            var hotelDtos = new List<HotelDto>();

            foreach (var hotel in hotels)
            {
                var pendingCount = await _unitOfWork.AdRequests.GetPendingCountAsync(hotel.Id);
                var hotelDto = _mapper.Map<HotelDto>(hotel);
                hotelDto.PendingAdsCount = pendingCount;
                hotelDtos.Add(hotelDto);
            }

            return hotelDtos;
        }

        public async Task<HotelDto?> GetHotelByIdAsync(Guid id, Guid userId)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(id, userId);
            if (hotel == null) return null;

            var pendingCount = await _unitOfWork.AdRequests.GetPendingCountAsync(hotel.Id);
            var hotelDto = _mapper.Map<HotelDto>(hotel);
            hotelDto.PendingAdsCount = pendingCount;

            return hotelDto;
        }

        public async Task<HotelDto> CreateHotelAsync(CreateHotelDto dto, Guid userId)
        {
            var hotelEntity = _mapper.Map<Hotel>(dto);
            hotelEntity.UserId = userId;

            var addedHotel = await _unitOfWork.Hotels.AddAsync(hotelEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HotelDto>(addedHotel);
        }

        public async Task<HotelDto?> UpdateHotelAsync(Guid id, UpdateHotelDto dto, Guid userId)
        {
            var existingHotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(id, userId);
            if (existingHotel == null) return null;

            _mapper.Map(dto, existingHotel);
            existingHotel.UpdatedAt = DateTime.UtcNow;

            var updatedHotel = await _unitOfWork.Hotels.UpdateAsync(existingHotel);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HotelDto>(updatedHotel);
        }

        public async Task<bool> DeleteHotelAsync(Guid id, Guid userId)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(id, userId);
            if (hotel == null) return false;

            // Check if hotel can be deleted (business logic)
            if (hotel.Rooms?.Count > 0)
                throw new InvalidOperationException("Cannot delete hotel with existing rooms");

            var result = await _unitOfWork.Hotels.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
