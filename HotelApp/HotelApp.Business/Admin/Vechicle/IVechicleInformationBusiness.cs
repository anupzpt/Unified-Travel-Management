using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Vechicle
{
    public interface IVechicleInformationBusiness
    {
        VechicleCommon GetRequiredDetails();
        DbResponse ManageVechicleDetail(VechicleCommon vechicleCommon);
        DbResponse ManageVechicleStatusDetail(object param);
        VechicleCommon GetVechicleInformationDetails(object param);
        Task<List<VechicleCommon>> GetRequiredDetailList(object gridParam);
        Task<List<VechicleCommon>> GetVechicleTypeList(GridParam gridParam);
    }
}
