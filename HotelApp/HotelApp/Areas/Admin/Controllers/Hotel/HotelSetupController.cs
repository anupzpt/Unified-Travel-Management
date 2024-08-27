using HotelApp.Business.Admin.HotelSetup;
using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Models;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Security.Cryptography.Xml;

namespace HotelApp.Areas.Admin.Controllers.Hotel
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class HotelSetupController : Controller
    {
        private readonly IHotelSetupBusiness _hotelSetupBusiness;
        private readonly IFileHelperExtension _fileHelperExtension;

        public HotelSetupController(IHotelSetupBusiness hotelSetupBusiness, IFileHelperExtension fileHelperExtension)
        {
            _hotelSetupBusiness = hotelSetupBusiness;
            _fileHelperExtension = fileHelperExtension;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetHotelAccomodationList(GridDetails param)
        {
            var gridParam = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetHotelAccomodationList",
                Search = param.search.value,
            };
            var gridList = await _hotelSetupBusiness.GetHotelAccomodationList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("HotelSetup", item.AccommodationType);
            }
            HtmlGrid<HotelCommon> companyGrid = new HtmlGrid<HotelCommon>();
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
        public IActionResult HotelList(string accommodationType)
        {
            var details = new HotelCommon();
            details.AccommodationType = accommodationType;
            return View(details);
        }
        [HttpPost]
        public async Task<string> GetGridDetails(GridDetails param, string AccommodationType)
        {
            var gridParam = new
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetGridDetails",
                Search = param.search.value,
                AccommodationType = AccommodationType
            };
            var gridList = await _hotelSetupBusiness.GetGridDetails(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("HotelInformation", item.HotelCode);
            }
            HtmlGrid<HotelCommon> companyGrid = new HtmlGrid<HotelCommon>();
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
        public IActionResult ManageHotelSetup()
        {
            var details = _hotelSetupBusiness.GetRequiredDetails();
            details.HotelAvailabilityList = new List<HotelAvailabilityDetails>();
            details.HotelPropertySurroundingList = new List<HotelPropertySurroundingDetails>();
            details.HotelFacilityList = new List<HotelFacilityDetails>();
            return View(details);
        }
        [HttpPost]
        public IActionResult AddHotelSetup(HotelCommon hotelCommon)
        {
            hotelCommon.Flag = "AddHotelInformationDetails";
            hotelCommon.HotelAvailabilityJson = JsonConvert.SerializeObject(hotelCommon.HotelAvailabilityList);
            hotelCommon.HotelPropertySurroundingJson = JsonConvert.SerializeObject(hotelCommon.HotelPropertySurroundingList);
            hotelCommon.HotelFacilityJson = JsonConvert.SerializeObject(hotelCommon.HotelFacilityList);
            var response = _hotelSetupBusiness.ManageHotelSetupDetails(hotelCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateHotelSetup(string code)
        {
            var param = new
            {
                Flag = "GetHotelDetails",
                HotelCode = StaticData.Base64Decode_URL(code),
            };
            var details = _hotelSetupBusiness.GetHotelDetails(param);
            return View("ManageHotelSetup", details);
        }
        [HttpPost]
        public IActionResult UpdateHotelSetup(HotelCommon hotelCommon)
        {
            hotelCommon.Flag = "UpdateHotelInformationDetails";
            hotelCommon.HotelAvailabilityJson = JsonConvert.SerializeObject(hotelCommon.HotelAvailabilityList);
            hotelCommon.HotelPropertySurroundingJson = JsonConvert.SerializeObject(hotelCommon.HotelPropertySurroundingList);
            hotelCommon.HotelFacilityJson = JsonConvert.SerializeObject(hotelCommon.HotelFacilityList);
            var response = _hotelSetupBusiness.ManageHotelSetupDetails(hotelCommon);
            return RedirectToAction("Index").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UploadHotelGallery(string code)
        {
            var param = new
            {
                Flag = "GetHotelGalleryDetails",
                HotelCode = StaticData.Base64Decode_URL(code)
            };
            var response = _hotelSetupBusiness.GetHotelGalleryDetails(param);
            return View(response);
        }
        [HttpPost]
        public IActionResult UploadHotelGallery(HotelCommon hotelCommon)
        {
            var documentList = new List<dynamic>();

            for (int i = 0; i < hotelCommon.HotelGalleryList.Count(); i++)
            {
                var hotelImage = hotelCommon.HotelGalleryList[i].HotelGalleryFile != null ? _fileHelperExtension.UploadFile(hotelCommon.HotelGalleryList[i].HotelGalleryFile).Result : hotelCommon.HotelGalleryList[i].HotelGalleryView;
                var documentItem = new
                {
                    Image = hotelImage,
                };
                documentList.Add(documentItem);
            }
            var param = new
            {
                Flag = "UploadHotelGallery",
                HotelGalleryJson = JsonConvert.SerializeObject(documentList),
                HotelCode = hotelCommon.HotelCode
            };
            var response = _hotelSetupBusiness.ManageGalleryDetail(param);
            return RedirectToAction("Index");
        }

        public IActionResult HotelBookedList()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetBookedHotelList(GridDetails param)
        {
            var gridParam = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetBookedHotelList",
                Search = param.search.value,
            };
            var gridList = await _hotelSetupBusiness.GetBookedList(gridParam);
            HtmlGrid<HotelBookedList> companyGrid = new HtmlGrid<HotelBookedList>();
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

        public IActionResult PackageBookedList()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetBookedPackagelList(GridDetails param)
        {
            var gridParam = new GridParam
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetBookedPackagelList",
                Search = param.search.value,
            };
            var gridList = await _hotelSetupBusiness.GetBookedList(gridParam);
            HtmlGrid<HotelBookedList> companyGrid = new HtmlGrid<HotelBookedList>();
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
    }
}
