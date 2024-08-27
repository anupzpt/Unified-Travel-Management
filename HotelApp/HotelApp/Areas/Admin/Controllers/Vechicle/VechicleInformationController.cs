using HotelApp.Business.Admin.Vechicle;
using HotelApp.Models;
using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Areas.Admin.Controllers.Vechicle
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
 
 
	public class VechicleInformationController : Controller
    {
        private readonly IVechicleInformationBusiness _vechicleInformationBusiness;
        public VechicleInformationController(IVechicleInformationBusiness vechicleInformationBusiness)
        {
            _vechicleInformationBusiness = vechicleInformationBusiness;
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
                Flag = "GetVechicleTypeList",
                Search = param.search.value,
            };
            var gridList = await _vechicleInformationBusiness.GetVechicleTypeList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("VechicleSetup", item.VechicleType);
            }
            HtmlGrid<VechicleCommon> companyGrid = new HtmlGrid<VechicleCommon>();
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

        public IActionResult VechicleIndex(string VechicleType)
        {
            var details = new VechicleCommon();
            details.VechicleType = VechicleType;
            return View(details);
        }
        [HttpPost]
        public async Task<string> GetRequiredVechicleDetailList(GridDetails param, string VechicleType)
        {
            var gridParam = new 
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetRequiredDetailList",
                Search = param.search.value,
                VechicleType = VechicleType
            };
            var gridList = await _vechicleInformationBusiness.GetRequiredDetailList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("VechicleTypeSetup", item.VechicleCode);
            }
            HtmlGrid<VechicleCommon> companyGrid = new HtmlGrid<VechicleCommon>();
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
        public IActionResult ManageVechicleDetail()
        {
            var details = _vechicleInformationBusiness.GetRequiredDetails();
            details.VechicleFeatureDetails = new List<VechicleFeatureDetail>();
            return View(details);
        }
        [HttpPost]
        public IActionResult AddVechicleDetail(VechicleCommon vechicleCommon)
        {
            vechicleCommon.Flag = "AddVechicleDetail";
            vechicleCommon.VechicleFeatureJson = JsonConvert.SerializeObject(vechicleCommon.VechicleFeatureDetails);
            var response= _vechicleInformationBusiness.ManageVechicleDetail(vechicleCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(),response.Message);   
        }
        public IActionResult UpdateVechicleDetail(string Code)
        {
            var param = new
            {
                VechicleCode = StaticData.Base64Decode_URL(Code),
                Flag= "GetVechicleInformationDetails",
            };
            var response = _vechicleInformationBusiness.GetVechicleInformationDetails(param);
            return View("ManageVechicleDetail", response);
        }
        [HttpPost]
        public IActionResult UpdateVechicleDetail(VechicleCommon vechicleCommon)
        {
            vechicleCommon.Flag = "UpdateVechicleDetail";
            vechicleCommon.VechicleFeatureJson = JsonConvert.SerializeObject(vechicleCommon.VechicleFeatureDetails);
            var response = _vechicleInformationBusiness.ManageVechicleDetail(vechicleCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateVechicleStatusDetail(string Code) {
            var param = new
            {
                VechicleCode= StaticData.Base64Decode_URL(Code),
                Flag= "UpdateVechicleStatusDetail"
            };
            var response = _vechicleInformationBusiness.ManageVechicleStatusDetail(param);
            return RedirectToAction("Index");
        }
    }
}
