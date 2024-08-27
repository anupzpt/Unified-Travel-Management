using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Guide
{
    public class GuideCommon : CommonModel
    {
        public string GuideCode { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string CitizenshipNo { get; set; }
        public string PhoneNo { get; set; }
        public string SpecializedRegion { get; set; }
        public string Experience { get; set; }
        public string Description { get; set; }
        public string GuideImageView { get; set; }
        public IFormFile GuideImageFile { get; set; }

    }
}
