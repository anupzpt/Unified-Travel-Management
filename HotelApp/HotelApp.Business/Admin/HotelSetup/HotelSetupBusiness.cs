using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.HotelSetup
{
    public class HotelSetupBusiness : IHotelSetupBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;

        private static string StoreProcedureName = "Setting.PROC_HotelManagement";
        public HotelSetupBusiness(IGenericRepository genericRepository, IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }

        public DbResponse ManageHotelSetupDetails(HotelCommon hotelCommon)
        {
            var hotelImage = hotelCommon.HotelImageFile != null ? _fileHelperExtension.UploadFile(hotelCommon.HotelImageFile).Result : hotelCommon.HotelImageView;
            var param = new
            {
                Flag = hotelCommon.Flag,
                HotelName = hotelCommon.HotelName,
                Location = hotelCommon.Location,
                Description = hotelCommon.Description,
                ReviewCode = hotelCommon.ReviewCode,
                DocumentCode = hotelCommon.DocumentCode,
                AccommodationType = hotelCommon.AccommodationType,
                HotelImageView = hotelImage,
                ContactNo = hotelCommon.ContactNo,
                AlternativeContactNo = hotelCommon.AlternativeContactNo,
                HotelAvailabilityJson = hotelCommon.HotelAvailabilityJson,
                HotelPropertySurroundingJson = hotelCommon.HotelPropertySurroundingJson,
                HotelFacilityJson = hotelCommon.HotelFacilityJson,
                HotelCode = hotelCommon.HotelCode,
            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public HotelCommon GetRequiredDetails()
        {
            var details = new HotelCommon();
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoreProcedureName, "GetRequiredDetails");
            details.AccommodationTypeList = response[0];
            return details;
        }
        public async Task<List<HotelCommon>> GetGridDetails(object gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<HotelCommon>(StoreProcedureName, gridParam);
            return list;
        }
        public async Task<List<HotelCommon>> GetHotelAccomodationList(GridParam gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<HotelCommon>(StoreProcedureName, gridParam);
            return list;
        }
        public HotelCommon GetHotelGalleryDetails(object param)
        {
            var details = new HotelCommon();
            var response = _genericRepository.ManageDataWithListObjectMultiple<HotelCommon, HotelGalleryDetails>(StoreProcedureName, param);
            details = ((List<HotelCommon>)response[0])[0];
            details.HotelGalleryList = (List<HotelGalleryDetails>)response[1];
            return details;
        }
        public DbResponse ManageGalleryDetail(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public HotelCommon GetHotelDetails(object param)
        {
            var details = new HotelCommon();
            var response = _genericRepository.ManageDataWithListObjectMultiple<HotelCommon, HotelAvailabilityDetails, HotelPropertySurroundingDetails, HotelFacilityDetails>(StoreProcedureName, param);
            details = ((List<HotelCommon>)response[0])[0];
            details.HotelAvailabilityList = (List<HotelAvailabilityDetails>)response[1];
            details.HotelPropertySurroundingList = (List<HotelPropertySurroundingDetails>)response[2];
            details.HotelFacilityList = (List<HotelFacilityDetails>)response[3];
            return details;
        }
        public async Task<List<HotelBookedList>> GetBookedList(GridParam gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<HotelBookedList>(StoreProcedureName, gridParam);
            return list;
        }
    }
}
