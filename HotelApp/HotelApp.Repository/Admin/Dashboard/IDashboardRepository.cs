using HotelApp.Shared.Admin.Dashboard;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Repository.Admin.Dashboard
{
    public interface IDashboardRepository
    {
        DashboardCommon GetDashboardDetail();
    }
}
