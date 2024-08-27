using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Gallery
{
    public class GalleryCommon
    {
        public List<GalleryDetails> GalleryDetailList { get; set; }
    }
    public class GalleryDetails
    {
        public string RowId { get; set; }
        public string GalleryView { get; set; }
        public IFormFile GalleryFile { get; set; }
    }
}
