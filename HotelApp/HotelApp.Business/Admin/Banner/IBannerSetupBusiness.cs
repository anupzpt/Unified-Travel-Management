using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Banner
{
    public interface IBannerSetupBusiness
    {
        DbResponse ManageBannerDetails(BannerCommon bannerCommon);
        BannerCommon GetBannerSetupDetails(object param);
        Task<List<BannerCommon>> GetRequiredDetailList(GridParam gridParam);
        DbResponse ManageBannerStaus(object param);
    }
}
