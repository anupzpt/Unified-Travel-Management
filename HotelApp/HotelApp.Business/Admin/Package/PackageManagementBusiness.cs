using HotelApp.Repository.Generic;
using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Package
{
    public class PackageManagementBusiness : IPackageManagementBusiness
    {
        private readonly IGenericRepository _genericRepository;
        private static string StoreProcedureName = "Setting.PackageInformationManagement";
        public PackageManagementBusiness(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public async Task<List<PackageDetails>> GetPackageTypeList(GridParam gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<PackageDetails>(StoreProcedureName, gridParam);
            return list;
        }
        public async Task<List<PackageDetails>> GetPackageInformationList(object gridParam)
        {
            var list = _genericRepository.ManageDataWithListObject<PackageDetails>(StoreProcedureName, gridParam);
            return list;
        }

        public PackageCommon GetRequiredDetails()
        {
            var detail = new PackageCommon();
            var response = _genericRepository.ManageDataWithMultipleSelectListItem(StoreProcedureName, "GetRequiredDetails");
            detail.PackageTypeList = response[0];
            detail.GroupTypeList = response[1];
            detail.GuideList = response[2];
            return detail;
        }
        public List<PackageCommon> GetPackageListByPackageType(object param)
        {
            var response = _genericRepository.ManageDataWithListObject<PackageCommon>(StoreProcedureName, param);
            return response;
        }
        public PackageCommon GetPackageDetailByPackageType(object param)
        {
            var details = new PackageCommon();
            var response = _genericRepository.ManageDataWithListObjectMultiple<PackageCommon, PackageItineraryDetails, PackageAvailabilityDetails, InclusionExcludesPackageDetails>(StoreProcedureName, param);
            details = ((List<PackageCommon>)response[0])[0];
            details.PackageItineraries = (List<PackageItineraryDetails>)response[1];
            details.PackageAvailabilities = (List<PackageAvailabilityDetails>)response[2];
            details.InclusionExcludesPackage = (List<InclusionExcludesPackageDetails>)response[3];
            return details;
        }
        public List<object> LoadAutocomplete(string type, string param1)
        {
            var param = new
            {
                Flag = type,
                Search = param1
            };
            var response = _genericRepository.ManageDataWithListObject<object>(StoreProcedureName, param);
            return response;
        }
        public DbResponse ManagePackageDetails(PackageParamDetail param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public PackageCommon GetDetailsForPayment(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<PackageCommon>(StoreProcedureName, param);
            return details;
        }
        public object CalculatePackageInformation(object param)
        {
            var details = _genericRepository.ManageDataWithSingleObject<object>(StoreProcedureName, param);
            return details;
        }
        public DbResponse SuccessPayment(object param)
        {
            var response = _genericRepository.ManageData(StoreProcedureName, param);
            return response;
        }
        public PackageCommon GetPackageDetails(object param)
        {
            var details = new PackageCommon();
            var list = GetRequiredDetails();
            var response = _genericRepository.ManageDataWithListObjectMultiple<PackageCommon, PackageItineraryDetails, PackageAvailabilityDetails, InclusionExcludesPackageDetails>(StoreProcedureName, param);
            details = ((List<PackageCommon>)response[0])[0];
            details.PackageItineraries = (List<PackageItineraryDetails>)response[1];
            details.PackageAvailabilities = (List<PackageAvailabilityDetails>)response[2];
            details.InclusionExcludesPackage = (List<InclusionExcludesPackageDetails>)response[3];
            details.PackageTypeList = list.PackageTypeList;
            details.GroupTypeList = list.GroupTypeList;
            details.GuideList = list.GuideList;
            return details;
        }
    }
}
