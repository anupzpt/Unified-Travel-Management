using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Gallery;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Gallery
{
    public class GalleryBusiness : IGalleryBusiness
    {
        private readonly IGenericRepository _genericRepository;
        public static string StoreProceduredName = "Setting.GalleryManagement";
        public GalleryBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public DbResponse ManageGalleryDetails(object param)
        {
            var response = _genericRepository.ManageData(StoreProceduredName,param);
            return response;
        }
        public GalleryCommon GetGalleryDetails()
        {
            var details = new GalleryCommon();
            var param = new
            {
                Flag = "GetGalleryDetails",
            };
            var response = _genericRepository.ManageDataWithListObject<GalleryDetails>(StoreProceduredName, param);
            details.GalleryDetailList = response;
            return details;
        }
        public DbResponse RemoveImage(object param)
        {
            var response = _genericRepository.ManageData(StoreProceduredName, param);
            return response;
        }
    }
}
