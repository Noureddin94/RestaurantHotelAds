using Microsoft.AspNetCore.Identity;
using RestaurantHotelAds.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Entities
{
    /// <summary>
    /// Application user that extends ASP.NET Identity user (Guid primary key).
    /// Uuid is a globally unique identifier useful for linking domain entities.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Global unique id used across domain entities (Hotels, Restaurants, etc).
        /// </summary>
        //public Guid Uuid { get; set; } = Guid.NewGuid();
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; } = true;
        public UserRole Role { get; set; } = UserRole.Guest;

        // Navigation properties
        public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
        public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    }
}
