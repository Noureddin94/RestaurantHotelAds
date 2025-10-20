using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string? Floor { get; set; }
        public string? RoomType { get; set; }
        public string? DisplayDeviceId { get; set; }
        public bool IsActive { get; set; }
        public int ActiveAdsCount { get; set; }
    }

    public class CreateRoomDto
    {
        [Required(ErrorMessage = "Room number is required")]
        [StringLength(50)]
        public string RoomNumber { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Floor { get; set; }

        [StringLength(50)]
        public string? RoomType { get; set; }

        [StringLength(200)]
        public string? DisplayDeviceId { get; set; }
    }

    public class UpdateRoomDto
    {
        [Required(ErrorMessage = "Room number is required")]
        [StringLength(50)]
        public string RoomNumber { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Floor { get; set; }
        [StringLength(50)]
        public string? RoomType { get; set; }
        [StringLength(200)]
        public string? DisplayDeviceId { get; set; }
        public bool IsActive { get; set; }
    }

    public class DeleteRoomDto
    {
        [Required(ErrorMessage = "Room ID is required")]
        public int Id { get; set; }
    }
}