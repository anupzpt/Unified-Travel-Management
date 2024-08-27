using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.UtilityHelper.FileHelperExtension
{
    public interface IFileHelperExtension
    {
        Task<string> UploadFile(IFormFile iFormFile);
        Task<MemoryStream> DownloadFile(string fileName);
        string GetImageForDisplay(string fileName);
    }
}
