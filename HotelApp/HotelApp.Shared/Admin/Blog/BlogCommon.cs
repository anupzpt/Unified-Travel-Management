﻿using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Blog
{
    public class BlogCommon : CommonModel
    {
        public string BlogCode { get; set; }
        public string Title { get; set; }
        public string SloganName { get; set; }
        public string Description { get; set; }
        public string BlogImageView { get; set; }
        public string CreatedDate { get; set; }
        public IFormFile BlogImageFile { get; set; }
        public string UserName { get; set; }
    }
}
