using HotelApp.Repository.Admin.Dashboard;
using HotelApp.Shared.Admin.Dashboard;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Dashboard
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardBusiness(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }   
        public DashboardCommon GetDashboardDetail()
        {
            var response = _dashboardRepository.GetDashboardDetail();
            return response;
        }
    }
}
