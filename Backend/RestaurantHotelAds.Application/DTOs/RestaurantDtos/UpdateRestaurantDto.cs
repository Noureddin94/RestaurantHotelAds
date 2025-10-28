using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.RestaurantDtos
{
    public class UpdateRestaurantDto
    {
        [Required(ErrorMessage = "Restaurant name is required")]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)] 
        public string? Description { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(300, MinimumLength = 5)]
        public string Address { get; set; } = string.Empty;
        [Phone]
        public string? ContactPhone { get; set; }
        [EmailAddress]
        public string? ContactEmail { get; set; }
        public bool IsActive { get; set; }
    }
}
