using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Admin.Vechicle;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Vechicle
{
    public class VechicleInformationBusiness : IVechicleInformationBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;
        private static string StoreProcedureName = "Setting.Proc_VechicleInformationManagement";
        public VechicleInformationBusiness(IGenericRepository genericRepository, IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }
        public VechicleCommon GetRequiredDetails()
        {
            var vechicleViewModel = new VechicleCommon();
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoreProcedureName, "GetRequiredDetails");
            vechicleViewModel.VechicleTypeList = response[0];
            return vechicleViewModel;

        }
        public DbResponse ManageVechicleDetail(VechicleCommon vechicleCommon)
        {
            var vechicleImage = vechicleCommon.VechicleImageFile != null ? _fileHelperExtension.UploadFile(vechicleCommon.VechicleImageFile).Result : vechicleCommon.VechicleImageView;
            var param = new
            {
                VechicleImageView = vechicleImage,
                Flag = vechicleCommon.Flag,
                VechicleCode = vechicleCommon.VechicleCode,
                VechicleType = vechicleCommon.VechicleType,
                Name = vechicleCommon.Name,
                Brand = vechicleCommon.Brand,
                Model = vechicleCommon.Model,
                Capacity = vechicleCommon.Capacity,
                RegistrationNo = vechicleCommon.RegistrationNo,
                VechicleFeatureJson = vechicleCommon.VechicleFeatureJson,
                Description = vechicleCommon.Description
            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public DbResponse ManageVechicleStatusDetail(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public VechicleCommon GetVechicleInformationDetails(object param)
        {
            var model = new VechicleCommon();
            var vechicleType = GetRequiredDetails();
            var details = _genericRepository.ManageDataWithListObjectMultiple<VechicleCommon, VechicleFeatureDetail>(StoreProcedureName, param);
            model = ((List<VechicleCommon>)details[0])[0];
            model.VechicleFeatureDetails = (List<VechicleFeatureDetail>)details[1];
            model.VechicleTypeList = vechicleType.VechicleTypeList;
            return model;
        }
        public async Task<List<VechicleCommon>> GetRequiredDetailList(object gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<VechicleCommon>(StoreProcedureName, gridParam);
            return list;
        }
        public async Task<List<VechicleCommon>> GetVechicleTypeList(GridParam gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<VechicleCommon>(StoreProcedureName, gridParam);
            return list;
        }
    }
}
