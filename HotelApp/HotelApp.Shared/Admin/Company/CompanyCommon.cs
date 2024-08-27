using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Company
{
    public class CompanyCommon : CommonModel
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Description { get; set; }
        public string PopularServices { get; set; }
        public string CompanyImageView { get; set; }
        public IFormFile CompanyImageFile { get; set; }
    }
}
