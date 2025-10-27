using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.RoomAdvertisementDtos
{
    public class CreateRoomAdvertisementDto
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
}
