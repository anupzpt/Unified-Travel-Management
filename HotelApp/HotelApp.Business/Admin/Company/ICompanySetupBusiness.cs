using HotelApp.Shared.Admin.Company;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Company
{
    public interface ICompanySetupBusiness
    {
        DbResponse ManageCompanyDetails(CompanyCommon companyCommon);
        CompanyCommon GetCompanySetupDetails();
    }
}
