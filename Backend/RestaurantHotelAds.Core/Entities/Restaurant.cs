using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    public class Restaurant : BaseEntity
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
        public bool IsActive { get; set; } = true;

        // Foreign Key to ApplicationUser
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public virtual ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
    }
}
