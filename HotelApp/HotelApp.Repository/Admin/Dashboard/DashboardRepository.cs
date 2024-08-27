using HotelApp.Repository.Dao;
using HotelApp.Shared.Admin.Dashboard;
using HotelApp.Shared.Common;
using iSolutionLife.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Repository.Admin.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private static string Storeprocedure = "Setting.Proc_DashbaordManagement";
        private readonly IDapperDao _dapperDao;
        public DashboardRepository(IDapperDao dapperDao)
        {
            _dapperDao = dapperDao;
        }
        public DashboardCommon GetDashboardDetail()
        {
            var param = new
            {

                Flag = "GetDashboardDetail",
            };
            var response = new DashboardCommon();
            //var response = _dapperDao.ExecuteQuery<DashboardCommon>(Storeprocedure, param);
            return response;

        }

    }
}
