using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class RoomAdvertisement
    {
        public int Id { get; set; }
        public int AdRequestId { get; set; }
        public int RoomId { get; set; }
        public int DisplayOrder { get; set; } = 1;
        public int DurationSeconds { get; set; } = 10;
        public bool IsActive { get; set; } = true;
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        public virtual AdRequest AdRequest { get; set; } = null!;
        public virtual Room Room { get; set; } = null!;
    }
}
