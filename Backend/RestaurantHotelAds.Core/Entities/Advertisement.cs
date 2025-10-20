using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class Advertisement
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? MediaUrl { get; set; }
        public string MediaType { get; set; } = "Image"; // Image, Video
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; } = "Draft"; // Draft, Submitted, Active
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<AdRequest> AdRequests { get; set; } = new List<AdRequest>();
    }
}
