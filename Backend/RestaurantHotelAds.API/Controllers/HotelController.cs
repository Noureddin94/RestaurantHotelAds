using Microsoft.AspNetCore.Mvc;

namespace RestaurantHotelAds.API.Controllers
{
    public class HotelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
