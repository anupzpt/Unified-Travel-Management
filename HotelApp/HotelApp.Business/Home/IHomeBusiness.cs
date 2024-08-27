using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using HotelApp.Shared.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Home
{
    public interface IHomeBusiness
    {
        HomeCommon GetDashboardDetails();
        Task<List<HomeDetails>> GetBookingList(GridParam gridParam);
        BlogCommon GetBlogPostDetails(object param);
        PackageCommon GetBookedPackageDetails(object param);
        HotelCommon GetBookedHotelDetails(object param);
    }
}
