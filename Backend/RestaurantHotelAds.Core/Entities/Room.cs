using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class Room : BaseEntity
    {
        [Required]
        public Guid HotelId { get; set; }

        [Required]
        [StringLength(50)]
        public string RoomNumber { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Floor { get; set; }

        [StringLength(50)]
        public string? RoomType { get; set; }

        [StringLength(200)]
        public string? DisplayDeviceId { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<RoomAdvertisement> RoomAdvertisements { get; set; } = new List<RoomAdvertisement>();
    }
}
