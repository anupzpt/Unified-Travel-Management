using HotelApp.Shared.Admin;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Repository.Admin.FacilitySetup
{
    public interface IFacilitySetupRepository
    {
        List<FacilityDetails> GetRequiredDetailList(GridParam gridParam);
        DbResponse ManageFacilitySetup(FacilityCommon facilityCommon);
        FacilityCommon GetFacilityDetails(object facilityParam);
    }
}
