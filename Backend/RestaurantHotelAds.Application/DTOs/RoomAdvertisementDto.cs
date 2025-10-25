using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs
{
    public class RoomAdvertisementDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string AdTitle { get; set; } = string.Empty;
        public string? AdMediaUrl { get; set; }
        public int DisplayOrder { get; set; }
        public int DurationSeconds { get; set; }
        public bool IsActive { get; set; }
        public DateTime AssignedAt { get; set; }
    }

    public class CreateRoomAdvertisementDto
    {
        public int AdRequestId { get; set; }
        public int RoomId { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public int DurationSeconds { get; set; } = 10;
    }

    public class UpdateRoomAdvertisementDto
    {
        public int DisplayOrder { get; set; }
        public int DurationSeconds { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoomAdvertisementFilterDto
    {
        public int? RoomId { get; set; }
        public bool? IsActive { get; set; }
    }
}
