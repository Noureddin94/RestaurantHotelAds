using AutoMapper;
using RestaurantHotelAds.Application.DTOs.RoomDtos;
using RestaurantHotelAds.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.RoomsServices
{
    public class RoomsService : IRoomsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetAllHotelRoomsAsync(Guid hotelId, Guid userId)
        {
            var rooms = await _unitOfWork.Rooms.GetAllByHotelIdAndUserIdAsync(hotelId, userId);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);

        }
        public async Task<IEnumerable<RoomDto>> GetHotelRoomsAsync(Guid hotelId)
        {
            var rooms = await _unitOfWork.Rooms.GetByHotelIdAsync(hotelId);
            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto> GetRoomByIdAsync(Guid roomId, Guid userId)
        {
            var room = await _unitOfWork.Rooms.GetByIdWithHotelAsync(roomId);
            if (room == null || room.Hotel.UserId != userId)
            {
                throw new KeyNotFoundException("Room not found or access denied.");
            }
            return _mapper.Map<RoomDto>(room);
        }
        public async Task<RoomDto> CreateRoomAsync(Guid hotelId, DTOs.RoomAdvertisementDtos.CreateRoomAdvertisementDto dto, Guid userId)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAndUserIdAsync(hotelId, userId);
            if (hotel == null)
            {
                throw new KeyNotFoundException("Hotel not found or access denied.");
            }
            var roomEntity = _mapper.Map<Core.Entities.Room>(dto);
            roomEntity.HotelId = hotelId;
            var createdRoom = await _unitOfWork.Rooms.AddAsync(roomEntity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<RoomDto>(createdRoom);
        }
        public async Task<RoomDto?> UpdateHotelRoomsAsync(Guid id, DTOs.RoomDtos.UpdateRoomDto dto, Guid userId)
        {
            var room = await _unitOfWork.Rooms.GetByIdWithHotelAsync(id);
            if (room == null || room.Hotel.UserId != userId)
            {
                return null;
            }
            _mapper.Map(dto, room);
            room.UpdatedAt = DateTime.UtcNow;
            var updatedRoom = await _unitOfWork.Rooms.UpdateAsync(room);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<RoomDto>(updatedRoom);
        }
        public async Task<bool> DeleteRoomAsync(Guid roomId, Guid userId)
        {
            var room = await _unitOfWork.Rooms.GetByIdWithHotelAsync(roomId);
            if (room == null || room.Hotel.UserId != userId)
            {
                return false;
            }
            return await _unitOfWork.Rooms.DeleteAsync(roomId);
        }
    }
}
