using HotelApp.Repository.Generic;
using HotelApp.Repository.Home;
using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Admin.Gallery;
using HotelApp.Shared.Admin.Guide;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using HotelApp.Shared.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Home
{
    public class HomeBusiness : IHomeBusiness
    {
        public readonly IHomeRepository _homeRepository;
        private readonly IGenericRepository _genericRepository;
        private static string StoreProcedureName="Setting.Proc_HomePageManagement";
        public HomeBusiness(IHomeRepository homeRepository,IGenericRepository genericRepository)
        {
            _homeRepository = homeRepository;
            _genericRepository = genericRepository;
        }
        public HomeCommon GetDashboardDetails()
        {
            var details = new HomeCommon();
            var param = new
            {
                Flag= "GetDashboardDetails"
            };
            var response = _genericRepository.ManageDataWithListObjectMultiple<BannerCommon,PackageCommon,GalleryDetails,BlogCommon,GuideCommon>(StoreProcedureName, param);
            details.BannerDetailsList = (List<BannerCommon>)response[0];
            details.PackageDetailsList = (List<PackageCommon>)response[1];
            details.GalleryDetailsList = (List<GalleryDetails>)response[2];
            details.BlogDetailList = (List<BlogCommon>)response[3];
            details.GuideDetailList = (List<GuideCommon>)response[4];
            return details;
        }
        public async Task<List<HomeDetails>> GetBookingList(GridParam gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<HomeDetails>(StoreProcedureName, gridParam);
            return list;
        }
        public BlogCommon GetBlogPostDetails(object param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<BlogCommon>(StoreProcedureName, param);
            return response;
        }
        public PackageCommon GetBookedPackageDetails(object param)
        {
            var details = new PackageCommon();
            var response = _genericRepository.ManageDataWithListObjectMultiple<PackageCommon,PackageItineraryDetails>(StoreProcedureName, param);
            details = ((List<PackageCommon>)response[0])[0];
            details.PackageItineraries = (List<PackageItineraryDetails>)response[1];
            return details;
        }
        public HotelCommon GetBookedHotelDetails(object param)
        {
            var response = _genericRepository.ManageDataWithSingleObject<HotelCommon>(StoreProcedureName,param);
            return response;
        }
    }
}
