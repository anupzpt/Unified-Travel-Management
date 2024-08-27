using HotelApp.Business.Admin.Driver;
using HotelApp.Models;
using HotelApp.Shared.Admin.Driver;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Areas.Admin.Controllers.Driver
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
	public class DriverSetupController : Controller
    {
        private readonly IDriverSetupBusiness _driverBusiness;
        public DriverSetupController(IDriverSetupBusiness driverBusiness)
        {
            _driverBusiness = driverBusiness;
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
            };
            var gridList = await _driverBusiness.GetRequiredDetailList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("DriverSetup", item.DriverCode);
            }
            HtmlGrid<DriverCommon> companyGrid = new HtmlGrid<DriverCommon>();
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

        public IActionResult ManageDriverSetup()
        {
            var details = new DriverCommon();
            return View(details);
        }
        [HttpPost]
        public IActionResult AddDriverSetup(DriverCommon DriverCommon)
        {
            DriverCommon.Flag = "AddDriverDetails";
            var response = _driverBusiness.ManageDriverDetails(DriverCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateDriverSetup(string code)
        {
            var param = new
            {
                Flag = "GetDriverDetails",
                DriverCode = StaticData.Base64Decode_URL(code),
            };
            var detail = _driverBusiness.GetDriverSetupDetails(param);
            return View("ManageDriverSetup", detail);
        }
        [HttpPost]
        public IActionResult UpdateDriverSetup(DriverCommon DriverCommon)
        {
            DriverCommon.Flag = "UpdateDriverDetails";
            var response = _driverBusiness.ManageDriverDetails(DriverCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateDriverStatus(string code)
        {
            var param = new
            {
                Flag = "UpdateDriverStatus",
                DriverCode = StaticData.Base64Decode_URL(code),
            };
            var response = _driverBusiness.ManageDriverStaus(param);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
    }
}
