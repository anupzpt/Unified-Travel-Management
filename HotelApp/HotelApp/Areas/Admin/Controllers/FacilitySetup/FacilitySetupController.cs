using HotelApp.Business.Admin.FacilitySetup;
using HotelApp.Shared.Admin;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HotelApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace HotelApp.Areas.Admin.Controllers.FacilitySetup
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
	public class FacilitySetupController : Controller
    {
        private readonly IFacilitySetupBusiness _facilitySetupBusiness;
        public FacilitySetupController(IFacilitySetupBusiness facilitySetupBusiness)
        {
            _facilitySetupBusiness = facilitySetupBusiness;
        }
        #region Facility Category
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
            var gridList = await _facilitySetupBusiness.GetRequiredDetailList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("FacilitySetup", item.CategoryCode);
            }
            HtmlGrid<FacilityDetails> companyGrid = new HtmlGrid<FacilityDetails>();
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
        public IActionResult ManageFacilitySetup()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFacilitySetup(FacilityCommon facilityCommon)
        {
            facilityCommon.Flag = "AddFacilitySetup";
            facilityCommon.UserName = StaticData.GetUser(HttpContext);
            var response= _facilitySetupBusiness.ManageFacilitySetup(facilityCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateFacilitySetup(string code)
        {
            var param = new 
            {
                Flag="GetFacilityDetails",
                CategoryCode=StaticData.Base64Decode_URL(code)
            };
            var details = _facilitySetupBusiness.GetFacilityDetails(param);
            return View("ManageFacilitySetup", details);   
        }
        [HttpPost]
        public IActionResult UpdateFacilitySetup(FacilityCommon facilityCommon)
        {
            facilityCommon.Flag = "UpdateFacilitySetup";
            facilityCommon.UserName = StaticData.GetUser(HttpContext);
            var response = _facilitySetupBusiness.ManageFacilitySetup(facilityCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        #endregion

        #region Facility Setuo
        public IActionResult SetupList()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetRequiredSetupDetailList(GridDetails param)
        {
            var gridParam = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetRequiredSetupDetailList",
                Search = param.search.value,
                UserName = StaticData.GetUser(HttpContext)
            };
            var gridList = await _facilitySetupBusiness.GetRequiredDetailList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("FacilitySetup", item.CategoryCode);
            }
            HtmlGrid<FacilityDetails> companyGrid = new HtmlGrid<FacilityDetails>();
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
        public IActionResult ManageFacilitySetupDetails()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFacilitySetupDetails(FacilityCommon facilityCommon)
        {
            facilityCommon.Flag = "AddFacilitySetupDetails";
            facilityCommon.UserName = StaticData.GetUser(HttpContext);
            var response = _facilitySetupBusiness.ManageFacilitySetup(facilityCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateFacilitySetupDetails(string code)
        {
            var param = new 
            {
                Flag = "GetFacilitySetupDetails",
                CategoryCode = StaticData.Base64Decode_URL(code)
            };
            var details = _facilitySetupBusiness.GetFacilityDetails(param);
            return View("ManageFacilitySetup", details);
        }
        [HttpPost]
        public IActionResult UpdateFacilitySetupDetails(FacilityCommon facilityCommon)
        {
            facilityCommon.Flag = "UpdateFacilitySetupDetails";
            facilityCommon.UserName = StaticData.GetUser(HttpContext);
            var response = _facilitySetupBusiness.ManageFacilitySetup(facilityCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        #endregion

    }
}
