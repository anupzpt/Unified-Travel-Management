using HotelApp.Shared.Admin.Guide;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Guide
{
    public interface IGuideSetupBusiness
    {
        Task<List<GuideCommon>> GetGuideList(GridParam gridParam);
        GuideCommon GetRequiredDetails();
        DbResponse ManageGuideDetails(GuideCommon guideCommon);
        GuideCommon GetGuideDetails(object guideCommon);
    }
}
