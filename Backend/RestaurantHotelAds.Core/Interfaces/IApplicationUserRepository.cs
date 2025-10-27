using RestaurantHotelAds.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Interfaces
{
    public interface IApplicationUserRepository
    {
        // By Identity primary key (Guid)
        Task<ApplicationUser?> GetByIdAsync(Guid id);
        // By email
        Task<ApplicationUser?> GetByEmailAsync(string email);

        // Add new user (using UserManager<ApplicationUser> is preferred)
        Task<ApplicationUser> AddAsync(ApplicationUser user);

        // Update user
        Task<ApplicationUser> UpdateAsync(ApplicationUser user);
        // Delete user
        Task<ApplicationUser> DeleteAsync(ApplicationUser user);

        // Existence checks
        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByUuidAsync(Guid uuid);
    }
}
