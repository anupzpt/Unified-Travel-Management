using HotelApp.Business.Admin.Package;
using HotelApp.Business.Verification;
using HotelApp.Service;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;

namespace HotelApp.Controllers.Package
{
    public class PackageController : Controller
    {
        private readonly MailService _mailService;
        private readonly IVerificationBusiness _verificationBusiness;
        private readonly IPackageManagementBusiness _packageManagementBusiness;
        public PackageController(MailService mailService, IPackageManagementBusiness packageManagementBusiness, IVerificationBusiness verificationBusiness)
        {
            _mailService = mailService;
            _packageManagementBusiness = packageManagementBusiness;
            _verificationBusiness = verificationBusiness;

        }
        public IActionResult PackageList(string id)
        {
            var param = new
            {
                Flag = "GetPackageListByPackageType",
                PackageType = id
            };
            var response = _packageManagementBusiness.GetPackageListByPackageType(param);
            return View("Index", response);
        }
        public IActionResult PackageInformation(string id)
        {
            var param = new
            {
                Flag = "GetPackageDetailByPackageType",
                PackageCode = id
            };
            var response = _packageManagementBusiness.GetPackageDetailByPackageType(param);
            return View("PackageViewInformation", response);
        }
        [HttpPost]
        public IActionResult CalculatePackageInformation(string BookingDate, string PackageCode)
        {
            var param = new
            {
                Flag = "CalculatePackageInformation",
                BookingDate = BookingDate,
                PackageCode = PackageCode
            };
            var response = _packageManagementBusiness.CalculatePackageInformation(param);
            return Json(response);
        }
        public IActionResult BookNow(string id)
        {
            var loginCheck = StaticData.CheckLoginStatus(HttpContext);
            if (loginCheck == "111" || loginCheck == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var param = new
                {
                    Flag = "GetDetailsForPayment",
                    PackageCode = id
                };
                var response = _packageManagementBusiness.GetDetailsForPayment(param);
                Random random = new Random();
                response.RandomNumber = random.Next(1000, 10000).ToString();
                return View("PackageBookingView", response);
            }
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
                VerificationType = "Booking"
            };
            var response = _verificationBusiness.RegisterVerificationCode(param);
            return Json("Success");
        }
        public JsonResult MakePayment(string code)
        {
            var UserName = StaticData.GetUser(HttpContext);
            var param = new
            {
                Flag = "CheckVerificationCode",
                UserName = UserName,
                VerificationCode = code,
                VerificationType = "Booking"
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
        public IActionResult SuccessPayment(string code)
        {
            var param = new
            {
                Flag = "SuccessPayment",
                PackageCode = code,
                UserName = StaticData.GetUser(HttpContext),
            };
            var response = _packageManagementBusiness.SuccessPayment(param);
            return View();
        }
    }
}
