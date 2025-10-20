using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs
{
    public class ReviewAdRequestDto
    {
        [Required]
        public string Status { get; set; } = string.Empty; // "Approved" or "Rejected"

        [StringLength(500)]
        public string? ReviewNotes { get; set; }
    }
}
