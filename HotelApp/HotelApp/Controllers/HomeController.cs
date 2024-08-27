using HotelApp.Business.Admin.Package;
using HotelApp.Business.Home;
using HotelApp.Business.Login;
using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Business.Verification;
using HotelApp.Models;
using HotelApp.Service;
using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using HotelApp.Shared.Home;
using HotelApp.Shared.Login;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace HotelApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeBusiness _homeBusiness;
        private readonly IVerificationBusiness _verificationBusiness;
        private readonly ILoginBusiness _loginBusiness;
        private readonly MailService _mailService;
        private readonly IPackageManagementBusiness _packageManagementBusiness;
        private readonly IFileHelperExtension _fileHelperExtension;

        public HomeController(IHomeBusiness homeBusiness, IVerificationBusiness verificationBusiness, MailService mailService, ILoginBusiness loginBusiness, IPackageManagementBusiness packageManagementBusiness)
        {
            _homeBusiness = homeBusiness;
            _verificationBusiness = verificationBusiness;
            _mailService = mailService;
            _loginBusiness = loginBusiness;
            _packageManagementBusiness = packageManagementBusiness;
        }

        public IActionResult Index()
        {
            var response = _homeBusiness.GetDashboardDetails();
            return View(response);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Accomodation()
        {
            return View();
        }

        public IActionResult Package()
        {
            return View();
        }
        public IActionResult Activities()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Guide()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Review()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<JsonResult> VerifyUserDetails(string EmailId, string FullName)
        {
            Random random = new Random();
            int randomCode = random.Next(1000, 10000);
            string randomNumber = randomCode.ToString();
            await _mailService.SendEmailAsync(
                toEmail: EmailId,
                subject: "Account Identity Verification",
                body: randomNumber,
                logoPath: "wwwroot/Admin/assets/img/layouts/logo2_2cc3.png",
                FullName: FullName
            );
            var UserName = StaticData.GetUser(HttpContext);
            var param = new
            {
                Flag = "RegisterVerificationCode",
                UserName = UserName,
                VerificationCode = randomNumber,
                VerificationType = "Profile"
            };
            var response = _verificationBusiness.RegisterVerificationCode(param);
            return Json("Success");
        }
        public JsonResult CheckVerificationCode(string code)
        {
            var UserName = StaticData.GetUser(HttpContext);
            var param = new
            {
                Flag = "CheckVerificationCode",
                UserName = UserName,
                VerificationCode = code,
                VerificationType = "Profile"
            };
            var response = _verificationBusiness.CheckVerificationCode(param);
            if (response.ErrorCode.ToString() == "0")
            {
                return Json("0");
            }
            else
            {
                return Json("1");
            }
        }
        [HttpPost]
        public IActionResult Profile(LoginCommon loginCommon)
        {
            loginCommon.Flag = "UpdateUserInformation";
            var response = _loginBusiness.ManageUserDetails(loginCommon);
            if (response.ErrorCode.ToString() == "0")
            {
                HttpContext.Session.Clear();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult PlanATrip()
        {
            var loginCheck = StaticData.CheckLoginStatus(HttpContext);
            if (loginCheck == "111" || loginCheck == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var details = _packageManagementBusiness.GetRequiredDetails();
                details.PackageItineraries = new List<PackageItineraryDetails>();
                details.PackageAvailabilities = new List<PackageAvailabilityDetails>();
                details.InclusionExcludesPackage = new List<InclusionExcludesPackageDetails>();
                return View(details);
            }
        }
        [HttpPost]
        public IActionResult AddPackageDetails(PackageCommon packageCommon)
        {
            var param = new PackageParamDetail()
            {
                Flag = "AddPackageDetails",
                PackageName = packageCommon.PackageName,
                PackageType = packageCommon.PackageType,
                PackageAverageCost = packageCommon.PackageAverageCost,
                Country = packageCommon.Country,
                Duration = packageCommon.Duration,
                BestSeason = packageCommon.BestSeason,
                Group = packageCommon.Group,
                MinimumPerson = packageCommon.MinimumPerson,
                MaximumPerson = packageCommon.MaximumPerson,
                MaxAltiude = packageCommon.MaxAltiude,
                Accommodation = packageCommon.Accommodation,
                Transportation = packageCommon.Transportation,
                PackageCount = packageCommon.PackageCount,
                Description = packageCommon.Description,
                GuideCode = packageCommon.GuideCode,
                PackageItinerariesJson = JsonConvert.SerializeObject(packageCommon.PackageItineraries),
                PackageAvailabilitiesJson = JsonConvert.SerializeObject(packageCommon.PackageAvailabilities),
                InclusionExcludesPackageJson = JsonConvert.SerializeObject(packageCommon.InclusionExcludesPackage),
                UserName = StaticData.GetUser(HttpContext),
            };
            var response = _packageManagementBusiness.ManagePackageDetails(param);
            return RedirectToAction("Index");
        }
        public IActionResult MyBookingList()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetRequiredDetailList(GridDetails param)
        {
            var gridParam = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetBookingList",
                Search = param.search.value,
                UserName = StaticData.GetUser(HttpContext)
            };
            var gridList = await _homeBusiness.GetBookingList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("BookingList", item.Code,item.Status,item.CodeType);
            }
            HtmlGrid<HomeDetails> companyGrid = new HtmlGrid<HomeDetails>();
            companyGrid.aaData = gridList;
            var firstDefault = gridList.FirstOrDefault();
            if (firstDefault != null)
            {
                companyGrid.iTotalDisplayRecords = Convert.ToInt32(firstDefault.FilterCount);
                companyGrid.iTotalRecords = Convert.ToInt32(firstDefault.FilterCount);
            }
            var result = JsonConvert.SerializeObject(companyGrid).ToString();
            return result;
        }
        public IActionResult BlogPostDetails(string id)
        {
            var param = new
            {
                Flag= "GetBlogPostDetails",
                BlogCode=id
            };
            var response = _homeBusiness.GetBlogPostDetails(param);
            return View(response);
        }

        public IActionResult Confirmation(string code)
        {
            var param = new
            {
                Flag = "GetBookedHotelDetails",
                HotelCode = StaticData.Base64Decode_URL(code)
            };
            var details = _homeBusiness.GetBookedHotelDetails(param);
            return View(details);
        }
        public IActionResult PackageBookedDetail(string code)
        {
            var param = new
            {
                Flag= "GetBookedPackageDetails",
                PackageCode=StaticData.Base64Decode_URL(code)
            };
            var details = _homeBusiness.GetBookedPackageDetails(param);
            return View(details);
        }
    }
}
