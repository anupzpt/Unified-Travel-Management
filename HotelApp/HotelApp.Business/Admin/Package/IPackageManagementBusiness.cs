using HotelApp.Shared.Admin.Package;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Package
{
    public interface IPackageManagementBusiness
    {
        Task<List<PackageDetails>> GetPackageTypeList(GridParam gridParam);
        Task<List<PackageDetails>> GetPackageInformationList(object gridParam);
        PackageCommon GetRequiredDetails();
        List<PackageCommon> GetPackageListByPackageType(object param);
        PackageCommon GetPackageDetailByPackageType(object param);
        List<object> LoadAutocomplete(string type, string param1);
        DbResponse ManagePackageDetails(PackageParamDetail param);
        PackageCommon GetDetailsForPayment(object param);
        object CalculatePackageInformation(object param);
        DbResponse SuccessPayment(object param);
        PackageCommon GetPackageDetails(object param);

    }
}
