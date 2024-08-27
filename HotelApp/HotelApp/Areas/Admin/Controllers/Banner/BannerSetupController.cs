using HotelApp.Business.Admin.Banner;
using HotelApp.Models;
using HotelApp.Shared.Admin;
using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Areas.Admin.Controllers.Banner
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
	public class BannerSetupController : Controller
    {
        private readonly IBannerSetupBusiness _bannerSetupBusiness;
        public BannerSetupController(IBannerSetupBusiness bannerSetupBusiness)
        {
            _bannerSetupBusiness=bannerSetupBusiness;
        }
        public IActionResult Index()
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
                Flag = "GetRequiredDetailList",
                Search = param.search.value,
                UserName = StaticData.GetUser(HttpContext)
            };
            var gridList = await _bannerSetupBusiness.GetRequiredDetailList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("BannerSetup", item.BannerCode);
            }
            HtmlGrid<BannerCommon> companyGrid = new HtmlGrid<BannerCommon>();
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

        public IActionResult ManageBannerSetup()
        {
            var details = new BannerCommon();
            return View(details);
        }
        [HttpPost]
        public IActionResult AddBannerSetup(BannerCommon bannerCommon)
        {
            bannerCommon.Flag = "AddBannerDetails";
            var response=_bannerSetupBusiness.ManageBannerDetails(bannerCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(),response.Message);
        }
        public IActionResult UpdateBannerSetup(string code)
        {
            var param = new
            {
                Flag = "GetBannerDetails",
                BannerCode = StaticData.Base64Decode_URL(code),
            };
            var detail = _bannerSetupBusiness.GetBannerSetupDetails(param);
            return View("ManageBannerSetup",detail);
        }
        [HttpPost]
        public IActionResult UpdateBannerSetup(BannerCommon bannerCommon)
        {
            bannerCommon.Flag = "UpdateBannerDetails";
            var response = _bannerSetupBusiness.ManageBannerDetails(bannerCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateBannerStatus(string code)
        {
            var param = new
            {
                Flag = "UpdateBannerStatus",
                BannerCode = StaticData.Base64Decode_URL(code),
            };
            var response = _bannerSetupBusiness.ManageBannerStaus(param);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
    }   
}
