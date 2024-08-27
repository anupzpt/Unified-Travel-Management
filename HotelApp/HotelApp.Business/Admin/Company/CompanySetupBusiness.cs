using HotelApp.Business.UtilityHelper.FileHelperExtension;
using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Company;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Company
{
    public class CompanySetupBusiness : ICompanySetupBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IFileHelperExtension _fileHelperExtension;

        private static string StoreProcedureName = "Setting.Proc_CompanyManagement";
        public CompanySetupBusiness(IGenericRepository genericRepository, IFileHelperExtension fileHelperExtension)
        {
            _genericRepository = genericRepository;
            _fileHelperExtension = fileHelperExtension;
        }
        public DbResponse ManageCompanyDetails(CompanyCommon companyCommon)
        {
            var companyImage = companyCommon.CompanyImageFile != null ? _fileHelperExtension.UploadFile(companyCommon.CompanyImageFile).Result : companyCommon.CompanyImageView;
            var param = new
            {
                CompanyImageView = companyImage,
                CompanyCode = companyCommon.CompanyCode,
                Flag = companyCommon.Flag,
                CompanyName = companyCommon.CompanyName,
                Description = companyCommon.Description,
                PopularServices = companyCommon.PopularServices
            };
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public CompanyCommon GetCompanySetupDetails()
        {
            var param = new
            {
                Flag = "GetCompanySetupDetails"
            };
            var details = _genericRepository.ManageDataWithSingleObject<CompanyCommon>(StoreProcedureName, param);
            return details;
        }
    }
}
