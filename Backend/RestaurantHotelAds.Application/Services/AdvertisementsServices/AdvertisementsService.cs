using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantHotelAds.Application.DTOs.AdvertisementDtos;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.AdvertisementsServices
{
    public class AdvertisementsService : IAdvertisementsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;


        public AdvertisementsService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<AdvertisementDto>> GetAllAdvertisementsAsync(Guid userId)
        {
            // Check if user is restaurant owner or admin
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            IEnumerable<Advertisement> advertisements;

            if (user.Role == Core.Enums.UserRole.Restaurant)
            {
                // Restaurant owners see only their ads
                advertisements = await _unitOfWork.Advertisements.GetByRestaurantOwnerIdAsync(userId);
            }
            else if (user.Role == Core.Enums.UserRole.Admin || user.Role == Core.Enums.UserRole.HotelOwner)
            {
                // Admins and hotel owners see all active ads
                advertisements = await _unitOfWork.Advertisements.GetByUserIdAsync(userId);
            }
            else
            {
                throw new UnauthorizedAccessException("User role not authorized to view advertisements");
            }

            var advertisementDtos = new List<AdvertisementDto>();

            foreach (var ad in advertisements)
            {
                var adDto = _mapper.Map<AdvertisementDto>(ad);
                adDto.RestaurantName = ad.Restaurant?.Name ?? "Unknown Restaurant";
                adDto.RequestsCount = ad.AdRequests?.Count ?? 0;
                advertisementDtos.Add(adDto);
            }

            return advertisementDtos;
        }

        public async Task<AdvertisementDto> GetAdvertisementByIdAsync(Guid advertisementId, Guid userId)
        {
            var advertisement = await _unitOfWork.Advertisements.GetByIdWithRestaurantAsync(advertisementId);
            if (advertisement == null)
                throw new KeyNotFoundException("Advertisement not found");

            // Authorization check - user must own the restaurant or be admin
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            if (user.Role != Core.Enums.UserRole.Admin &&
                advertisement.Restaurant.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to view this advertisement");
            }

            var advertisementDto = _mapper.Map<AdvertisementDto>(advertisement);
            advertisementDto.RestaurantName = advertisement.Restaurant?.Name ?? "Unknown Restaurant";
            advertisementDto.RequestsCount = advertisement.AdRequests?.Count ?? 0;

            return advertisementDto;
        }

        public async Task<AdvertisementDto> CreateAdvertisementAsync(CreateAdvertisementDto dto, Guid userId)
        {
            // Validate business rules
            if (dto.EndDate <= dto.StartDate)
                throw new ArgumentException("End date must be after start date");

            if (dto.StartDate < DateTime.UtcNow.Date)
                throw new ArgumentException("Start date cannot be in the past");

            // Get user's restaurant
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null || user.Role != Core.Enums.UserRole.Restaurant)
                throw new UnauthorizedAccessException("Only restaurant owners can create advertisements");

            var restaurant = await _unitOfWork.Restaurants.GetByUserIdAsync(userId);
            if (restaurant == null)
                throw new InvalidOperationException("Restaurant not found for this user");

            var advertisementEntity = _mapper.Map<Advertisement>(dto);
            advertisementEntity.RestaurantId = restaurant.Id;
            advertisementEntity.Status = "Draft"; // Start as draft

            var addedAdvertisement = await _unitOfWork.Advertisements.AddAsync(advertisementEntity);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _mapper.Map<AdvertisementDto>(addedAdvertisement);
            resultDto.RestaurantName = restaurant.Name;
            resultDto.RequestsCount = 0;

            return resultDto;
        }

        public async Task<AdvertisementDto?> UpdateAdvertisementAsync(Guid id, UpdateAdvertisementDto dto, Guid userId)
        {
            var existingAdvertisement = await _unitOfWork.Advertisements.GetByIdWithRestaurantAsync(id);
            if (existingAdvertisement == null)
                return null;

            // Authorization check
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            if (user.Role != Core.Enums.UserRole.Admin &&
                existingAdvertisement.Restaurant.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to update this advertisement");
            }

            // Business logic validation
            if (dto.EndDate.HasValue && dto.StartDate.HasValue &&
                dto.EndDate <= dto.StartDate)
            {
                throw new ArgumentException("End date must be after start date");
            }

            // Update only provided fields
            _mapper.Map(dto, existingAdvertisement);
            existingAdvertisement.UpdatedAt = DateTime.UtcNow;

            var updatedAdvertisement = await _unitOfWork.Advertisements.UpdateAsync(existingAdvertisement);
            await _unitOfWork.SaveChangesAsync();

            var resultDto = _mapper.Map<AdvertisementDto>(updatedAdvertisement);
            resultDto.RestaurantName = existingAdvertisement.Restaurant?.Name ?? "Unknown Restaurant";
            resultDto.RequestsCount = existingAdvertisement.AdRequests?.Count ?? 0;

            return resultDto;
        }

        public async Task<bool> DeleteAdvertisementAsync(Guid advertisementId, Guid userId)
        {
            var advertisement = await _unitOfWork.Advertisements.GetByIdWithRestaurantAsync(advertisementId);
            if (advertisement == null)
                return false;

            // Authorization check
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null) throw new UnauthorizedAccessException("User not found");

            if (user.Role != Core.Enums.UserRole.Admin &&
                advertisement.Restaurant.UserId != userId)
            {
                throw new UnauthorizedAccessException("Not authorized to delete this advertisement");
            }

            // Business rule: Cannot delete if there are pending requests
            if (advertisement.AdRequests?.Count > 0)
            {
                throw new InvalidOperationException("Cannot delete advertisement with pending requests");
            }

            var result = await _unitOfWork.Advertisements.DeleteAsync(advertisementId);
            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}
