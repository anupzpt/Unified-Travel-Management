using HotelApp.Shared.Admin.Driver;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Driver
{
    public interface IDriverSetupBusiness
    {
        DbResponse ManageDriverDetails(DriverCommon DriverCommon);
        DriverCommon GetDriverSetupDetails(object param);
        Task<List<DriverCommon>> GetRequiredDetailList(GridParam gridParam);
        DbResponse ManageDriverStaus(object param);

    }
}
