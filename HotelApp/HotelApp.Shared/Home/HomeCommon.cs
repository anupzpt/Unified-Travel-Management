using HotelApp.Shared.Admin.Banner;
using HotelApp.Shared.Admin.Blog;
using HotelApp.Shared.Admin.Common;
using HotelApp.Shared.Admin.Gallery;
using HotelApp.Shared.Admin.Guide;
using HotelApp.Shared.Admin.Hotel;
using HotelApp.Shared.Admin.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Home
{
    public class HomeCommon 
    {
        public List<BannerCommon> BannerDetailsList { get; set; }   
        public List<PackageCommon> PackageDetailsList { get; set; }   
        public List<GalleryDetails> GalleryDetailsList { get; set; }   
        public List<BlogCommon> BlogDetailList { get; set; }   
        public List<GuideCommon> GuideDetailList { get; set; }   
    }
    public class HomeDetails :CommonModel
    {
        public string Code {get;set;}
        public string CodeType {get;set;}
        public string Name {get;set;}
        public string TotalAmount {get;set;}
        public string Status {get;set;}
        public string BookedDate { get; set; }
    }
}
