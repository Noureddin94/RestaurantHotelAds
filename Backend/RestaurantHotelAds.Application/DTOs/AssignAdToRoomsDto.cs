using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Application.DTOs
{
    public class AssignAdToRoomsDto
    {
        [Required]
        public int AdRequestId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one room must be selected")]
        public List<int> RoomIds { get; set; } = new List<int>();

        [Range(1, 100)]
        public int DisplayOrder { get; set; } = 1;

        [Range(5, 300)]
        public int DurationSeconds { get; set; } = 10;
    }
}
