using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class AdRequest : BaseEntity
    {
        public Guid AdvertisementId { get; set; }
        public Guid HotelId { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
        public string? ReviewNotes { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReviewedAt { get; set; }
        public int? ReviewedBy { get; set; }

        public virtual Advertisement Advertisement { get; set; } = null!;
        public virtual Hotel Hotel { get; set; } = null!;
        public virtual ICollection<RoomAdvertisement> RoomAdvertisements { get; set; } = [];
    }
}
