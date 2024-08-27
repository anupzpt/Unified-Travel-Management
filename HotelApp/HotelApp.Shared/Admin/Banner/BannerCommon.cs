using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Banner
{
    public class BannerCommon:CommonModel
    {
        public string BannerCode { get; set; }
        public string Title { get; set; }
        public string SloganName { get; set; }
        public string Description { get; set; }
        public string BannerImageView { get; set; }
        public IFormFile BannerImageFile { get; set; }
    }
}
