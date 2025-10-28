using AutoMapper;
using RestaurantHotelAds.Application.DTOs.RestaurantDtos;
using RestaurantHotelAds.Core.Entities;
using RestaurantHotelAds.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Services.RestaurantsServices
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RestaurantService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get restaurant by ID
        /// </summary>
        public async Task<RestaurantDto?> GetRestaurantByIdAsync(Guid id, Guid userId)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAndUserIdAsync(id, userId);
            if (restaurant == null) return null;

            // Use AutoMapper
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            restaurantDto.ActiveAdsCount = restaurant.Advertisements.Count(a => a.Status == "Active");
            restaurantDto.PendingRequestsCount = 0; // TODO: Calculate from AdRequests

            return restaurantDto;
        }

        /// <summary>
        /// Create new restaurant
        /// </summary>
        public async Task<RestaurantDto> CreateRestaurantAsync(CreateRestaurantDto dto, Guid userId)
        {
            // Check if restaurant name already exists for this user
            var exists = await _unitOfWork.Restaurants.ExistsByNameAsync(userId, dto.Name);
            if (exists)
            {
                throw new InvalidOperationException($"A restaurant with the name '{dto.Name}' already exists");
            }

            // Use AutoMapper to create entity
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.UserId = userId;
            restaurant.CreatedAt = DateTime.UtcNow;
            restaurant.IsActive = true;

            await _unitOfWork.Restaurants.AddAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            // Map back to DTO
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            restaurantDto.AdvertisementsCount = 0;
            restaurantDto.ActiveAdsCount = 0;
            restaurantDto.PendingRequestsCount = 0;

            return restaurantDto;
        }

        /// <summary>
        /// Update existing restaurant
        /// </summary>
        public async Task<RestaurantDto?> UpdateRestaurantAsync(Guid id, UpdateRestaurantDto dto, Guid userId)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAndUserIdAsync(id, userId);
            if (restaurant == null) return null;

            // Check if new name conflicts with existing restaurant
            if (restaurant.Name != dto.Name)
            {
                var exists = await _unitOfWork.Restaurants.ExistsByNameAsync(userId, dto.Name);
                if (exists)
                {
                    throw new InvalidOperationException($"A restaurant with the name '{dto.Name}' already exists");
                }
            }

            // Use AutoMapper to update entity
            _mapper.Map(dto, restaurant);
            restaurant.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Restaurants.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();

            // Map back to DTO
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
            restaurantDto.ActiveAdsCount = restaurant.Advertisements.Count(a => a.Status == "Active");
            restaurantDto.PendingRequestsCount = 0;

            return restaurantDto;
        }

        /// <summary>
        /// Delete restaurant
        /// </summary>
        public async Task<bool> DeleteRestaurantAsync(Guid id, Guid userId)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAndUserIdAsync(id, userId);
            if (restaurant == null) return false;

            // Check if restaurant has active advertisements
            if (restaurant.Advertisements.Any(a => a.Status == "Active"))
            {
                throw new InvalidOperationException("Cannot delete restaurant with active advertisements");
            }

            await _unitOfWork.Restaurants.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Toggle restaurant active status
        /// </summary>
        public async Task<bool> ToggleRestaurantStatusAsync(Guid id, Guid userId)
        {
            var restaurant = await _unitOfWork.Restaurants.GetByIdAndUserIdAsync(id, userId);
            if (restaurant == null) return false;

            restaurant.IsActive = !restaurant.IsActive;
            restaurant.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Restaurants.UpdateAsync(restaurant);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
