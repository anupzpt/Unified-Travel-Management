using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Business.UtilityHelper.FileHelperExtension
{
    public class FileHelperExtension : IFileHelperExtension
    {
        private readonly IConfiguration _configuration;

        public FileHelperExtension(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> UploadFile(IFormFile iFormFile)

        {
            //string contentRootPath = _hostingEnvironment.ContentRootPath;
            //string webRootPath = _hostingEnvironment.WebRootPath;
            var folderPath = _configuration["ApplicationData:FilePath"];
            var now = DateTime.Now;
            var yearName = now.ToString("yyyy");
            var monthName = now.ToString("MM");
            var dayName = now.ToString("dd");
            var filePath = Path.Combine(folderPath, Path.Combine(yearName, Path.Combine(monthName, dayName)));
            var fileName = GetUniqueFileName(iFormFile.FileName);
            string returnFileName = Path.Combine(Path.Combine(yearName, Path.Combine(monthName, dayName)), fileName);
            if (iFormFile.Length > 3097152) //in mb 2mb
            {
                return "0";
            }
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            var path = Path.Combine(filePath, fileName);
            var stream = new FileStream(path, FileMode.Create);
            try
            {
                try
                {
                    await iFormFile.CopyToAsync(stream);
                    return returnFileName;
                }
                finally
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return "";
            }
        }

        public async Task<MemoryStream> DownloadFile(string fileName)
        {
            var folderPath = _configuration["ApplicationData:FilePath"];
            var path = Path.Combine(folderPath, fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            //return File(memory, GetContentType(path), Path.GetFileName(path));
            return memory;
        }

        public string GetImageForDisplay(string fileName)
        {
            try
            {
                var folderPath = _configuration["ApplicationData:FilePath"];
                var path = Path.Combine(folderPath, fileName);
                long size = (new FileInfo(path)).Length;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[size];
                try
                {
                    fs.Read(data, 0, (int)size);
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
                var imageBase64Data = Convert.ToBase64String(data);
                string imageDataUrl = string.Format("data:image/" + Path.GetExtension(fileName).Replace(".", "") + ";base64,{0}", imageBase64Data);
                return imageDataUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return "";
            }
        }

        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
