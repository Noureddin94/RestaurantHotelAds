using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class Advertisement : BaseEntity
    {
        [Required]
        public Guid RestaurantId { get; set; }
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        [StringLength(1000)]
        public string? Description { get; set; }
        [StringLength(500)]
        public string? MediaUrl { get; set; }
        [StringLength(50)]
        public string MediaType { get; set; } = "Image"; // Image, Video
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [StringLength(50)]
        public string Status { get; set; } = "Draft"; // Draft, Submitted, Active

        public virtual Restaurant Restaurant { get; set; } = null!;
        public virtual ICollection<AdRequest> AdRequests { get; set; } = new List<AdRequest>();
    }
}
