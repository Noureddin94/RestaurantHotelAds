using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.RestaurantDtos
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public Guid Uuid { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public bool IsActive { get; set; }
        public int ActiveAdsCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public int AdvertisementsCount { get; set; }
        public int PendingAdsCount { get; set; }
        public int PendingRequestsCount { get; set; }
        public int PendingAds { get; set; }
    }
}
