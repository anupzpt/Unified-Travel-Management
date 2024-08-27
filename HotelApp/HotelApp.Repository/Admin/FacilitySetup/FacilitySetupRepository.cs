using HotelApp.Repository.Dao;
using HotelApp.Shared.Admin;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Repository.Admin.FacilitySetup
{
    public class FacilitySetupRepository : IFacilitySetupRepository
    {
        private readonly IDapperDao _dapperDao;
        private static string StoreProcedureName = "Setting.Proc_FacilitySetupManagement";
        public FacilitySetupRepository(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }
        // Grid List  
        public List<FacilityDetails> GetRequiredDetailList(GridParam gridParam)
        {
            var response = _dapperDao.ExecuteQuery<FacilityDetails>(StoreProcedureName,gridParam);
            return response;
        }
        public DbResponse ManageFacilitySetup(FacilityCommon facilityCommon)
        {
            var response = _dapperDao.ExecuteQuery<DbResponse>(StoreProcedureName, facilityCommon)[0];
            return response;
        }
        public FacilityCommon GetFacilityDetails(object facilityParam)
        {
            var details = _dapperDao.ExecuteQuery<FacilityCommon>(StoreProcedureName, facilityParam)[0];
            return details;
        }
    }
}
