using Microsoft.AspNetCore.Mvc;
using HotelApp.Shared.Login;
using Newtonsoft.Json;
using HotelApp.Business.Login;
using HotelApp.Shared.Common;

namespace HotelApp.Controllers.Login
{
	public class LoginController : Controller
	{
		private readonly ILoginBusiness _loginBusiness;
		public LoginController(ILoginBusiness loginBusiness)
		{
			_loginBusiness = loginBusiness;
		}
		[HttpGet]
		public IActionResult Index()
		{
			var loginCheck = StaticData.CheckLoginStatus(HttpContext);
			if (loginCheck == "111" || loginCheck== null)
			{
				return View();
			}
			else
			{
				return RedirectToAction("Index", "Home");
			}
		}
		[HttpPost]
		public IActionResult Login(LoginCommon modelLogin)
		{
			modelLogin.UserPassword = StaticData.Base64Encode(modelLogin.UserPassword);
			modelLogin.Flag = "CheckUserLogin";
			modelLogin.KeepLoggedIn = (modelLogin.KeepLoggedIn.ToString().ToLower() == "true") ? "1" : "0";
			var param = _loginBusiness.CheckUserLogin(modelLogin);
			if (param.Code == "000")
			{
				HttpContext.Session.SetString("UserInfo", JsonConvert.SerializeObject(param));
				return RedirectToAction("Index", "Login");
			}
			else
			{
				return RedirectToAction("Index", "Login");
			}
		}
		public IActionResult RegisterPage()
		{
			var model = new LoginCommon();
			return View(model);
		}
		[HttpPost]
		public IActionResult RegisterPage(LoginCommon modelLogin)
		{
			modelLogin.Flag = "RegisterUserDetails";
			modelLogin.UserPassword = StaticData.Base64Encode(modelLogin.UserPassword);
			var details = _loginBusiness.ManageUserDetails(modelLogin);
			return RedirectToAction("Index", "Login");
		}
	}
}
