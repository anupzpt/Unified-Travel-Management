using HotelApp.Shared.Admin;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.FacilitySetup
{
    public interface IFacilitySetupBusiness
    {
        Task<List<FacilityDetails>> GetRequiredDetailList(GridParam gridParam);
        DbResponse ManageFacilitySetup(FacilityCommon facilityCommon);
        FacilityCommon GetFacilityDetails(object facilityParam);
    }
}
