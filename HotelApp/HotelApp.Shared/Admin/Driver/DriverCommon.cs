using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Driver
{
    public class DriverCommon : CommonModel
    {
        public string DriverCode { get; set; }
        public string FullName { get; set; }
        public string Age { get; set; }
        public string ContactNo { get; set; }
        public string LiscineNo { get; set; }
        public string Experience { get; set; }
        public string DriverImageView { get; set; }
        public IFormFile DriverImageFile { get; set; }
    }
}
