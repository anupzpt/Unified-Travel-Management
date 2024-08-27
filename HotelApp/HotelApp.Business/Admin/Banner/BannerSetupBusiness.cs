using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin;
using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Banner
{
    public class BannerSetupBusiness : IBannerSetupBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;

        private static string StoreProcedureName = "Setting.Proc_BannerManagement";
        public BannerSetupBusiness(IGenericRepository genericRepository,IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }   
        public DbResponse ManageBannerDetails(BannerCommon bannerCommon)
        {
            var bannerImage = bannerCommon.BannerImageFile != null ? _fileHelperExtension.UploadFile(bannerCommon.BannerImageFile).Result : bannerCommon.BannerImageView;
            var param = new
            {
                BannerImageView = bannerImage,
                Flag=bannerCommon.Flag,
                BannerCode=bannerCommon.BannerCode,
                Title =bannerCommon.Title,
                SloganName=bannerCommon.SloganName,
                Description=bannerCommon.Description
            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        } 
        public BannerCommon GetBannerSetupDetails(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<BannerCommon>(StoreProcedureName, param);
            return details;
        }
        public async Task<List<BannerCommon>> GetRequiredDetailList(GridParam gridParam)
        {
            var bannerList = _genericRepository.ManageDataWithListObject<BannerCommon>(StoreProcedureName,gridParam);
            return bannerList;
        }
        public DbResponse ManageBannerStaus(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
    }
}
