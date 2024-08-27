using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Vechicle
{
    public class VechicleCommon : CommonModel
    {
        public string  VechicleCode { get; set; }
        public string  VechicleType { get; set; }
        public string  Name { get; set; }
        public string  Brand { get; set; }
        public string  Model { get; set; }
        public string  Capacity { get; set; }
        public string  RegistrationNo { get; set; }
        public string VechicleFeatureJson { get; set; } 
        public string VechicleImageView { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public IFormFile VechicleImageFile { get; set; }
        public List<SelectListItem> VechicleTypeList { get; set; }
        public List<VechicleFeatureDetail> VechicleFeatureDetails { get; set; }
    }
    public class VechicleFeatureDetail
    {
        public string VechicleFeature { get; set; }

    }
}
