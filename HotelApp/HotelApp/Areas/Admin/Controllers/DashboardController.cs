using HotelApp.Business.Admin.Dashboard;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]


    public class DashboardController : Controller
    {
        private readonly IDashboardBusiness _dashboardBusiness;
        public DashboardController(IDashboardBusiness dashboardBusiness)
        {
            _dashboardBusiness = dashboardBusiness;
        }
        public IActionResult Index()
        {
            var loginCheck = StaticData.CheckLoginStatus(HttpContext);
            if (loginCheck == "111" || loginCheck == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var response = _dashboardBusiness.GetDashboardDetail();
                if (!string.IsNullOrEmpty(response.CompanyImageView))
                {
                    ViewData["CompanyImageView"] = response.CompanyImageView.ToString();
                    ViewData["CompanyName"] = response.CompanyName.ToString();
                }

                return View(response);
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
