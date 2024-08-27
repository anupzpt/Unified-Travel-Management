using HotelApp.Shared.Admin.Gallery;
using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.Admin.Gallery
{
    public interface IGalleryBusiness
    {
        DbResponse ManageGalleryDetails(object param);
        GalleryCommon GetGalleryDetails();
        DbResponse RemoveImage(object param);
    }
}
