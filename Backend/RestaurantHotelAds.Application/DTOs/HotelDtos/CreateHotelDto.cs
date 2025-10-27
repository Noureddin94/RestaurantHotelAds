using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.HotelDtos
{
    public class CreateHotelDto
    {
        [Required(ErrorMessage = "Hotel name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 200 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Address must be between 5 and 300 characters")]
        public string Address { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? ContactPhone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? ContactEmail { get; set; }

        [Range(1, 10000, ErrorMessage = "Total rooms must be between 1 and 10000")]
        public int TotalRooms { get; set; }
    }
}
