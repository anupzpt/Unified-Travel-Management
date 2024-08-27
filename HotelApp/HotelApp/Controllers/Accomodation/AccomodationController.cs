using HotelApp.Business.Accomodation;
using HotelApp.Business.Verification;
using HotelApp.Service;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers.Accomodation
{
    public class AccomodationController : Controller
    {
        private readonly IAccomodationBusiness _accomodationBusiness;
        private readonly IVerificationBusiness _verificationBusiness;
        private readonly MailService _mailService;

        public AccomodationController(IAccomodationBusiness accomodationBusiness, MailService mailService,IVerificationBusiness verificationBusiness)
        {
            _accomodationBusiness = accomodationBusiness;
            _mailService = mailService;
            _verificationBusiness = verificationBusiness;
        }
        public IActionResult HotelList(string id)
        {
            var param = new
            {
                Flag = "GetHotelListByAccomodationType",
                AccommodationType = id
            };
            var response = _accomodationBusiness.GetHotelListByAccomodationType(param);
            return View(response);
        }
        public IActionResult HotelInformation(string id)
        {
            var param = new
            {
                Flag= "GetHotelDetails",
                HotelCode=id
            };
            var response = _accomodationBusiness.GetHotelDetails(param);
            return View("HotelViewInformation",response);
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
                var listParam = new
                {
                    Flag = "GetRequiredList",
                    HotelCode = id
                };
                var details = _accomodationBusiness.GetRequiredDetails(listParam);
                var param = new
                {
                    Flag = "GetDetailsForPayment",
                    HotelCode = id
                };
                var response = _accomodationBusiness.GetDetailsForPayment(param);
                response.RoomTypeList = details.RoomTypeList;
                Random random = new Random();
                response.RandomNumber = random.Next(1000, 10000).ToString();
                return View("HotelBookingView", response);
            }
        }
        [HttpPost]
        public async Task<JsonResult> VerifyUserDetails(string EmailId,string FullName)
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
                VerificationType="Booking"
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
                HotelCode = code,
                UserName = StaticData.GetUser(HttpContext),
            };
            var response = _accomodationBusiness.SuccessPayment(param);
            return View();
        }
        [HttpPost]
        public IActionResult CalculateHotelInformation(string RoomType, string HotelCode)
        {
            var param = new
            {
                Flag = "CalculateHotelInformation",
                RoomType = RoomType,
                HotelCode = HotelCode
            };
            var response = _accomodationBusiness.CalculateHotelInformation(param);
            return Json(response);
        }
    }
}
