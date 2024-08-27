using HotelApp.Business.Admin.Guide;
using HotelApp.Business.Admin.Vechicle;
using HotelApp.Shared.Admin.Guide;
using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Areas.Admin.Controllers.Guide
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class GuideSetupController : Controller
    {

        private readonly IGuideSetupBusiness _guideSetupBusiness;
        public GuideSetupController(IGuideSetupBusiness guideSetupBusiness)
        {
            _guideSetupBusiness = guideSetupBusiness;
        }
        public IActionResult Index()
        {
            return View();
        }
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
            var gridList = await _guideSetupBusiness.GetGuideList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("GuideSetup", item.GuideCode);
            }
            HtmlGrid<GuideCommon> companyGrid = new HtmlGrid<GuideCommon>();
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
        public IActionResult ManageGuideDetail()
        {
            var details = new GuideCommon();
            return View(details);
        }
        public IActionResult AddGuideDetail(GuideCommon guideCommon)
        {
            guideCommon.Flag = "AddGuideDetail";
            var response = _guideSetupBusiness.ManageGuideDetails(guideCommon);
            return RedirectToAction("Index");
        }
        public IActionResult UpdateGuideDetail(string code)
        {
            var param = new
            {
                Flag = "GetGuideDetails",
                GuideCode = StaticData.Base64Decode_URL(code)
            };
            var details = _guideSetupBusiness.GetGuideDetails(param);
            return View("ManageGuideDetail", details);
        }
        [HttpPost]
        public IActionResult UpdateGuideDetail(GuideCommon guideCommon)
        {
            guideCommon.Flag = "UpdateGuideDetail";
            var response = _guideSetupBusiness.ManageGuideDetails(guideCommon);
            return RedirectToAction("Index", "GuideSetup");
        }
    }
}
