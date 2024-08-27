using HotelApp.Shared.Admin.Dashboard;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Dashboard
{
    public interface IDashboardBusiness
    {
        DashboardCommon GetDashboardDetail();
    }
}
