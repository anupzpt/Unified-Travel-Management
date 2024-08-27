using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin
{
    public class FacilityCommon : CommonModel
    {
        public string Flag { get; set; }
        public string FacilityName { get; set; }
        public string Descritpion { get; set; }
        public IFormFile FacilityLogoFile { get; set; }
        public string FacilityLogoView { get; set; }
        public string UserName { get; set; }    
    }
 
}
