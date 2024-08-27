using HotelApp.Business.Admin.Package;
using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Packaging.Core;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace HotelApp.Areas.Admin.Controllers.Package
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class PackageSetupController : Controller
    {
        private readonly IPackageManagementBusiness _packageManagementBusiness;
        private readonly IFileHelperExtension _fileHelperExtension;

        public PackageSetupController(IPackageManagementBusiness packageManagementBusiness, IFileHelperExtension fileHelperExtension)
        {
            _packageManagementBusiness = packageManagementBusiness;
            _fileHelperExtension = fileHelperExtension;
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
                Flag = "GetPackageTypeList",
                Search = param.search.value,
            };
            var gridList = await _packageManagementBusiness.GetPackageTypeList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("PackageSetup", item.PackageType);
            }
            HtmlGrid<PackageDetails> companyGrid = new HtmlGrid<PackageDetails>();
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
        public IActionResult InformationList(string PackageType)
        {
            var details = new PackageCommon();
            details.PackageType = PackageType;
            return View(details);
        }
        [HttpPost]
        public async Task<string> GetRequiredPackageDetailList(GridDetails param, string PackageType)
        {
            var gridParam = new
            {
                DisplayLength = param.length,
                DisplayStart = param.start,
                SortDir = param.order[0].dir,
                SortCol = param.order[0].column,
                Flag = "GetPackageInformationList",
                Search = param.search.value,
                PackageType = PackageType

            };
            var gridList = await _packageManagementBusiness.GetPackageInformationList(gridParam);
            foreach (var item in gridList)
            {
                item.Action = StaticData.GetActions("PackageInformation", item.PackageCode);
            }
            HtmlGrid<PackageDetails> companyGrid = new HtmlGrid<PackageDetails>();
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
        public IActionResult ManagePackageDetails()
        {
            var details = _packageManagementBusiness.GetRequiredDetails();
            details.PackageItineraries = new List<PackageItineraryDetails>();
            details.PackageAvailabilities = new List<PackageAvailabilityDetails>();
            details.InclusionExcludesPackage = new List<InclusionExcludesPackageDetails>();
            return View(details);
        }
        [HttpPost]
        public IActionResult AddPackageDetails(PackageCommon packageCommon)
        {
            var packageImage = packageCommon.PackageImageFile != null ? _fileHelperExtension.UploadFile(packageCommon.PackageImageFile).Result : packageCommon.PackageImageView;
            var routeImage = packageCommon.RouteImageFile != null ? _fileHelperExtension.UploadFile(packageCommon.RouteImageFile).Result : packageCommon.RouteImageView;
            var param = new PackageParamDetail()
            {
                Flag = "AddPackageDetails",
                PackageImageView = packageImage,
                RouteImageView = routeImage,
                PackageName = packageCommon.PackageName,
                PackageType = packageCommon.PackageType,
                PackageAverageCost = packageCommon.PackageAverageCost,
                Country = packageCommon.Country,
                Duration = packageCommon.Duration,
                BestSeason = packageCommon.BestSeason,
                Group = packageCommon.Group,
                MinimumPerson = packageCommon.MinimumPerson,
                MaximumPerson = packageCommon.MaximumPerson,
                MaxAltiude = packageCommon.MaxAltiude,
                Accommodation = packageCommon.Accommodation,
                Transportation = packageCommon.Transportation,
                PackageCount = packageCommon.PackageCount,
                Description = packageCommon.Description,
                GuideCode = packageCommon.GuideCode,
                PackageItinerariesJson = JsonConvert.SerializeObject(packageCommon.PackageItineraries),
                PackageAvailabilitiesJson = JsonConvert.SerializeObject(packageCommon.PackageAvailabilities),
                InclusionExcludesPackageJson = JsonConvert.SerializeObject(packageCommon.InclusionExcludesPackage),
                UserName = StaticData.GetUser(HttpContext),
            };
            var response = _packageManagementBusiness.ManagePackageDetails(param);
            return RedirectToAction("Index");
        }
        public IActionResult UpdatePackageDetails(string Code)
        {
            var param = new
            {
                FLag= "GetPackageDetails",
                PackageCode=StaticData.Base64Decode_URL(Code),
            };
            var details = _packageManagementBusiness.GetPackageDetails(param);
            return View("ManagePackageDetails",details);
        }
        [HttpPost]
        public IActionResult UpdatePackageDetails(PackageCommon packageCommon)
        {
            var packageImage = packageCommon.PackageImageFile != null ? _fileHelperExtension.UploadFile(packageCommon.PackageImageFile).Result : packageCommon.PackageImageView;
            var routeImage = packageCommon.RouteImageFile != null ? _fileHelperExtension.UploadFile(packageCommon.RouteImageFile).Result : packageCommon.RouteImageView;
            var param = new PackageParamDetail()
            {
                Flag = "UpdatePackageDetails",
                PackageImageView = packageImage,
                RouteImageView = routeImage,
                PackageName = packageCommon.PackageName,
                PackageType = packageCommon.PackageType,
                PackageAverageCost = packageCommon.PackageAverageCost,
                Country = packageCommon.Country,
                Duration = packageCommon.Duration,
                BestSeason = packageCommon.BestSeason,
                Group = packageCommon.Group,
                MinimumPerson = packageCommon.MinimumPerson,
                MaximumPerson = packageCommon.MaximumPerson,
                MaxAltiude = packageCommon.MaxAltiude,
                Accommodation = packageCommon.Accommodation,
                Transportation = packageCommon.Transportation,
                PackageCount = packageCommon.PackageCount,
                Description = packageCommon.Description,
                GuideCode = packageCommon.GuideCode,
                PackageItinerariesJson = JsonConvert.SerializeObject(packageCommon.PackageItineraries),
                PackageAvailabilitiesJson = JsonConvert.SerializeObject(packageCommon.PackageAvailabilities),
                InclusionExcludesPackageJson = JsonConvert.SerializeObject(packageCommon.InclusionExcludesPackage),
                UserName = StaticData.GetUser(HttpContext),
                PackageCode = packageCommon.PackageCode
            };
            var response = _packageManagementBusiness.ManagePackageDetails(param);
            return RedirectToAction("Index");
        }
        public IActionResult LoadAutocomplete(string type, string param)
        {
            var list = _packageManagementBusiness.LoadAutocomplete(type, param);
            return Json(list);
        }
    }
}
