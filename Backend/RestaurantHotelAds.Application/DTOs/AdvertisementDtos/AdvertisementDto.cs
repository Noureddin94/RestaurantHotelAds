using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.AdvertisementDtos
{
    public class AdvertisementDto
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(500)]
        public string? MediaUrl { get; set; }

        [StringLength(50)]
        public string MediaType { get; set; } = "Image";

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Draft";
        public string RestaurantName { get; set; } = string.Empty;
        public int RequestsCount { get; set; }
        public bool IsActive => Status == "Active" &&
                               StartDate <= DateTime.UtcNow &&
                               EndDate >= DateTime.UtcNow;
    }
}
