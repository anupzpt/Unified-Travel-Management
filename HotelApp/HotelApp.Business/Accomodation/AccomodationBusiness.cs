using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Accomodation
{
    public class AccomodationBusiness : IAccomodationBusiness
    {
        private readonly IGenericRepository _genericRepository;
        public static string StoreProcedureName = "Setting.PROC_HotelManagement";
        public AccomodationBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public List<HotelCommon> GetHotelListByAccomodationType(object param)
        {
            var response = _genericRepository.ManageDataWithListObject<HotelCommon>(StoreProcedureName, param);
            return response;
        }
        public HotelCommon GetHotelDetails(object param)
        {
            var details = new HotelCommon();
            var response = _genericRepository.ManageDataWithListObjectMultiple<HotelCommon, HotelAvailabilityDetails, HotelPropertySurroundingDetails, HotelFacilityDetails, HotelGalleryDetails>(StoreProcedureName,param);
            details = ((List<HotelCommon>)response[0])[0];
            details.HotelAvailabilityList = (List<HotelAvailabilityDetails>)response[1];
            details.HotelPropertySurroundingList = (List<HotelPropertySurroundingDetails>)response[2];
            details.HotelFacilityList = (List<HotelFacilityDetails>)response[3];
            details.HotelGalleryList = (List<HotelGalleryDetails>)response[4];
            return details;
        }
        public HotelCommon GetDetailsForPayment(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<HotelCommon>(StoreProcedureName, param);
            return details;
        }
        public HotelCommon GetRequiredDetails(object param)
        {
            var detail = new HotelCommon();
            var response = _genericRepository.ManageDataWithMultipleSelectList(StoreProcedureName, param);
            detail.RoomTypeList = response[0];
            return detail;
        }
        public object CalculateHotelInformation(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<object>(StoreProcedureName, param);
            return details;
        }
        public DbResponse SuccessPayment(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
    }
}
