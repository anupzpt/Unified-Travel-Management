using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.HotelSetup
{
    public interface IHotelSetupBusiness
    {
        DbResponse ManageHotelSetupDetails(HotelCommon hotelCommon);
        HotelCommon GetRequiredDetails();
        Task<List<HotelCommon>> GetHotelAccomodationList(GridParam gridParam);
        Task<List<HotelCommon>> GetGridDetails(object gridParam);
        HotelCommon GetHotelGalleryDetails(object param);
        DbResponse ManageGalleryDetail(object param);
        HotelCommon GetHotelDetails(object param);
        Task<List<HotelBookedList>> GetBookedList(GridParam gridParam);
    }
}
