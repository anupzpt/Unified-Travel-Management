using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Accomodation
{
    public interface IAccomodationBusiness
    {
        List<HotelCommon> GetHotelListByAccomodationType(object param);
        HotelCommon GetHotelDetails(object param);
        HotelCommon GetDetailsForPayment(object param);
        HotelCommon GetRequiredDetails(object param);
        object CalculateHotelInformation(object param);
        DbResponse SuccessPayment(object param);
    }
}
