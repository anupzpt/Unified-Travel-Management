using HotelApp.Business.Admin.Company;
using HotelApp.Models;
using HotelApp.Shared.Admin.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.Areas.Admin.Controllers.Company
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]/{id?}")]
 
	public class CompanySetupController : Controller
    {
        private readonly ICompanySetupBusiness _companySetupBusiness;
        public CompanySetupController(ICompanySetupBusiness companySetupBusiness)
        {
            _companySetupBusiness = companySetupBusiness;
        }
        public IActionResult ManageCompanySetup()
        {
            var details = _companySetupBusiness.GetCompanySetupDetails();
            if(details == null)
            {
                details = new CompanyCommon();
            }
            return View(details);
        }
        [HttpPost]
        public IActionResult AddCompanyDetails(CompanyCommon companyCommon)
        {
            companyCommon.Flag = "AddCompanyDetails";
            var response = _companySetupBusiness.ManageCompanyDetails(companyCommon);
            return RedirectToAction("ManageCompanySetup").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
        public IActionResult UpdateCompanyDetails(string CompanyCode)
        {
            var details = _companySetupBusiness.GetCompanySetupDetails();
            return View("ManageCompanySetup", details);
        }
        [HttpPost]
        public IActionResult UpdateCompanyDetails(CompanyCommon companyCommon)
        {
            companyCommon.Flag = "UpdateCompanyDetails";
            var response = _companySetupBusiness.ManageCompanyDetails(companyCommon);
            return RedirectToAction("ManageCompanySetup").WithAlertMessage(response.ErrorCode.ToString(), response.Message);
        }
    }
}
