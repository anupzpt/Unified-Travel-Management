using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Areas.Admin.Controllers.HotelInformationSetup
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
	public class HotelInformationSetupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
