using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs
{
    public class AdRequestDto
    {
        public int Id { get; set; }
        public int AdvertisementId { get; set; }
        public int HotelId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ReviewNotes { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? ReviewedAt { get; set; }

        // Advertisement details
        public string AdTitle { get; set; } = string.Empty;
        public string? AdDescription { get; set; }
        public string? AdMediaUrl { get; set; }
        public string AdMediaType { get; set; } = string.Empty;

        // Restaurant details
        public string RestaurantName { get; set; } = string.Empty;
        public string? RestaurantAddress { get; set; }
        public string? RestaurantContactPhone { get; set; }

        // Assignment info
        public int AssignedRoomsCount { get; set; }
    }
}
