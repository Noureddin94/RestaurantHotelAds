using AutoMapper;
using RestaurantHotelAds.Application.DTOs;
using RestaurantHotelAds.Application.DTOs.AdvertisementDtos;
using RestaurantHotelAds.Application.DTOs.AuthDtos;
using RestaurantHotelAds.Application.DTOs.HotelDtos;
using RestaurantHotelAds.Application.DTOs.RestaurantDtos;
using RestaurantHotelAds.Application.DTOs.RoomDtos;
using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Hotel
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.RoomsCount, opt => opt.MapFrom(src => src.Rooms.Count))
                .ForMember(dest => dest.PendingAdsCount, opt => opt.Ignore());

            // CreateHotelDto -> Hotel Entity
            CreateMap<CreateHotelDto, Hotel>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Rooms, opt => opt.Ignore())
                .ForMember(dest => dest.AdRequests, opt => opt.Ignore());

            // UpdateHotelDto -> Hotel Entity
            CreateMap<UpdateHotelDto, Hotel>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Rooms, opt => opt.Ignore())
                .ForMember(dest => dest.AdRequests, opt => opt.Ignore());

            // ========== ROOM MAPPINGS ==========

            // Room Entity -> RoomDto
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.ActiveAdsCount, opt => opt.MapFrom(src =>
                    src.RoomAdvertisements.Count(ra => ra.IsActive)))
                .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Hotel.Name));

            // CreateRoomDto -> Room Entity
            CreateMap<CreateRoomDto, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HotelId, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.Hotel, opt => opt.Ignore())
                .ForMember(dest => dest.RoomAdvertisements, opt => opt.Ignore());

            // UpdateRoomDto -> Room Entity
            CreateMap<UpdateRoomDto, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.HotelId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Hotel, opt => opt.Ignore())
                .ForMember(dest => dest.RoomAdvertisements, opt => opt.Ignore());

            // ========== RESTAURANT MAPPINGS ==========

            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.AdvertisementsCount, opt => opt.MapFrom(src =>
                    src.Advertisements.Count));

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateRestaurantDto, Restaurant>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            // ========== ADVERTISEMENT MAPPINGS ==========

            CreateMap<Advertisement, AdvertisementDto>()
                .ForMember(dest => dest.RestaurantName, opt => opt.MapFrom(src => src.Restaurant.Name))
                .ForMember(dest => dest.RequestsCount, opt => opt.MapFrom(src => src.AdRequests.Count));

            CreateMap<CreateAdvertisementDto, Advertisement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RestaurantId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Draft"));

            // ========== USER MAPPINGS ==========

            CreateMap<ApplicationUser, AuthResponseDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Set from roles in service

        }
    }
}
