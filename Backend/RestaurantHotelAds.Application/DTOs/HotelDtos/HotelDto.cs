using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs.HotelDtos
{
    public class HotelDto
    {
        public Guid Uuid { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Phone]
        public string? ContactPhone { get; set; }
        [EmailAddress]
        public string? ContactEmail { get; set; }
        public int TotalRooms { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int RoomsCount { get; set; }
        public int PendingAdsCount { get; set; }
    }

    //public class DeleteHotelDto
    //{
    //    [Required(ErrorMessage = "Hotel ID is required")]
    //    public int Id { get; set; }
    //}

    //public class HotelFilterDto
    //{
    //    public string? Name { get; set; }
    //    public bool? IsActive { get; set; }
    //}
}
