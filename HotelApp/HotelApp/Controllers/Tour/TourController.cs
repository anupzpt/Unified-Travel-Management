using HotelApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Controllers.Tour
{
    public class TourController : Controller
    {
        private readonly MailService _mailService;

        public TourController(MailService mailService)
        {
            _mailService = mailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SendTestEmail()
        {
            await _mailService.SendEmailAsync(
                toEmail: "prajapatianu433@gmail.com",
                subject: "Welcome to Our Service!",
                body: "We are glad to have you with us.",
                logoPath: "wwwroot/Admin/assets/img/layouts/logo2_2cc3.png",
                FullName:"Name"
            );
            return Ok("Email sent");
        }
        
    }
}
