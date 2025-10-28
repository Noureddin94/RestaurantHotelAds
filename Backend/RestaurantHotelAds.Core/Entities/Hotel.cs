using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class Hotel : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(300)]
        public string Address { get; set; } = string.Empty;

        [Phone]
        public string? ContactPhone { get; set; }

        [EmailAddress]
        public string? ContactEmail { get; set; }

        public int TotalRooms { get; set; }

        public bool IsActive { get; set; } = true;
        
        // Foreign Key to ApplicationUser
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        // Navigation properties
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        public virtual ICollection<AdRequest> AdRequests { get; set; } = new List<AdRequest>();
    }
}
