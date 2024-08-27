using HotelApp.Repository.Admin.FacilitySetup;
using HotelApp.Shared.Admin;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.FacilitySetup
{
    public class FacilitySetupBusiness : IFacilitySetupBusiness
    {
        private readonly IFacilitySetupRepository _facilitySetupRepository;
        public FacilitySetupBusiness(IFacilitySetupRepository facilitySetupRepository)
        {
            _facilitySetupRepository = facilitySetupRepository;
        }

        public async Task<List<FacilityDetails>> GetRequiredDetailList(GridParam gridParam)
        {
            var facilityList = _facilitySetupRepository.GetRequiredDetailList(gridParam);
            return facilityList;
        }
        public DbResponse ManageFacilitySetup(FacilityCommon facilityCommon)
        {
            var response = _facilitySetupRepository.ManageFacilitySetup(facilityCommon);
            return response;
        }
        public FacilityCommon GetFacilityDetails(object facilityParam)
        {
            var details = _facilitySetupRepository.GetFacilityDetails(facilityParam);
            return details;
        }
    }
}
