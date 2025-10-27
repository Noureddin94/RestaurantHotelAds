using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.RoomDtos
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string HotelName { get; set; } = string.Empty;
        public string RoomNumber { get; set; } = string.Empty;
        public string? Floor { get; set; }
        public string? RoomType { get; set; }
        public string? DisplayDeviceId { get; set; }
        public bool IsActive { get; set; }
        public int ActiveAdsCount { get; set; }
    }

    //public class DeleteRoomDto
    //{
    //    [Required(ErrorMessage = "Room ID is required")]
    //    public int Id { get; set; }
    //}
}
