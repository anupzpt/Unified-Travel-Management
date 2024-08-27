using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers.Booking
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
