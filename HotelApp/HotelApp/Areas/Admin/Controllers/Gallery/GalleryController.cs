using HotelApp.Business.Admin.Gallery;
using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Shared.Admin.Gallery;
using HotelApp.Shared.Admin.Hotel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelApp.Areas.Admin.Controllers.Gallery
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
    public class GalleryController : Controller
    {
        private readonly IFileHelperExtension _fileHelperExtension;
        private readonly IGalleryBusiness _galleryBusiness;

        public GalleryController(IFileHelperExtension fileHelperExtension,IGalleryBusiness galleryBusiness)
        {
            _fileHelperExtension = fileHelperExtension;
            _galleryBusiness = galleryBusiness;
        }
        public IActionResult Index()
        {
            var response = _galleryBusiness.GetGalleryDetails();
            return View(response);
        }
        [HttpPost]
        public IActionResult UploadGalleryDetail(GalleryCommon galleryCommon)
        {
            var documentList = new List<dynamic>();
            for (int i = 0; i < galleryCommon.GalleryDetailList.Count(); i++)
            {
                var galleryImage = galleryCommon.GalleryDetailList[i].GalleryFile != null ? _fileHelperExtension.UploadFile(galleryCommon.GalleryDetailList[i].GalleryFile).Result : galleryCommon.GalleryDetailList[i].GalleryView;
                var documentItem = new
                {
                    Image = galleryImage,
                };
                documentList.Add(documentItem);
            }
            var param = new
            {
                Flag = "UploadGalleryDetail",
                GalleryJson = JsonConvert.SerializeObject(documentList),
            };
            var response = _galleryBusiness.ManageGalleryDetails(param);
            return RedirectToAction("Index","Gallery");
        }
        public IActionResult RemoveImage(string id)
        {
            var param = new
            {
                Flag= "RemoveImage",
                RowId=id
            };
            var response = _galleryBusiness.RemoveImage(param);
            return Json(response);
        }
    }
}
