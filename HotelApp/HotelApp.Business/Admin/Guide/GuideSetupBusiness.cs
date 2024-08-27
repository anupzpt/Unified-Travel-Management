using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Driver;
using HotelApp.Shared.Admin.Guide;
using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelApp.Business.Admin.Guide
{
    public class GuideSetupBusiness : IGuideSetupBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;
        private static string StoreProcedureName = "Setting.Proc_GuideSetupManagement";
        public GuideSetupBusiness(IGenericRepository genericRepository, IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }
        public async Task<List<GuideCommon>> GetGuideList(GridParam gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<GuideCommon>(StoreProcedureName, gridParam);
            return list;
        }
        public GuideCommon GetRequiredDetails()
        {
            var guideViewModel = new GuideCommon();
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoreProcedureName, "GetRequiredDetails");
            return guideViewModel;

        }
        public DbResponse ManageGuideDetails(GuideCommon guideCommon)
        {
            var guideImage = guideCommon.GuideImageFile != null ? _fileHelperExtension.UploadFile(guideCommon.GuideImageFile).Result : guideCommon.GuideImageView;
            var param = new
            {
                GuideImageView = guideImage,
                Flag = guideCommon.Flag,
                Name=guideCommon.Name,
                Age=guideCommon.Age,
                Address=guideCommon.Address,
                CitizenshipNo=guideCommon.CitizenshipNo,
                PhoneNo=guideCommon.PhoneNo,
                SpecializedRegion=guideCommon.SpecializedRegion,
                Experience=guideCommon.Experience,
                Description=guideCommon.Description,
                GuideCode = guideCommon.GuideCode,
            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public GuideCommon GetGuideDetails(object guideCommon)
        {
            var details = _genericRepository.ManageDataWithSingleObject<GuideCommon>(StoreProcedureName, guideCommon);
            return details;
        }
    }
}
