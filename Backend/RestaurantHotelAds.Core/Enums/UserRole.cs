using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantHotelAds.Core.Enums
{
    public enum UserRole
    {
        Admin = 1,
        HotelOwner = 2,
        Restaurant = 3,
        staff = 4,
        customer = 5,
        Guest = 6
    }

    /// <summary>
    /// Extension methods for UserRole
    /// </summary>
    public static class UserRoleExtensions
    {
        public static string ToRoleName(this UserRole role)
        {
            return role.ToString();
        }

        public static UserRole FromRoleName(string roleName)
        {
            return Enum.Parse<UserRole>(roleName, ignoreCase: true);
        }

        public static List<string> GetAllRoleNames()
        {
            return Enum.GetValues<UserRole>()
                .Select(r => r.ToString())
                .ToList();
        }
    }
}
