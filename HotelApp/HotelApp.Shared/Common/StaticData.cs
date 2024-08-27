using HotelApp.Shared.Login;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Common
{
    public class StaticData
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static string GetUser(HttpContext context)
        {
            var userInfoJson = context.Session.GetString("UserInfo");
            if (userInfoJson != null)
            {
                var admin = JsonConvert.DeserializeObject<LoginCommon>(userInfoJson);
                var value = admin.Username;
                return value;
            }
            return null;
        }
        public static string GetFullName(HttpContext context)
        {
            var userInfoJson = context.Session.GetString("UserInfo");
            if (userInfoJson != null)
            {
                var admin = JsonConvert.DeserializeObject<LoginCommon>(userInfoJson);
                var value = admin.FullName;
                return value;
            }
            return null;
        }
        public static string GetContactNo(HttpContext context)
        {
            var userInfoJson = context.Session.GetString("UserInfo");
            if (userInfoJson != null)
            {
                var admin = JsonConvert.DeserializeObject<LoginCommon>(userInfoJson);
                var value = admin.ContactNo;
                return value;
            }
            return null;
        }
        public static string GetEmailId(HttpContext context)
        {
            var userInfoJson = context.Session.GetString("UserInfo");
            if (userInfoJson != null)
            {
                var admin = JsonConvert.DeserializeObject<LoginCommon>(userInfoJson);
                var value = admin.EmailId;
                return value;
            }
            return null;
        }
        public static string CheckLoginStatus(HttpContext context)
        {
            var userInfoJson = context.Session.GetString("UserInfo");
            if (userInfoJson != null)
            {
                var admin = JsonConvert.DeserializeObject<LoginCommon>(userInfoJson);
                var value = admin.Code;
                return value;
            }
            return "111";
        }
        public static string CheckIsAdmin(HttpContext context)
        {
            var userInfoJson = context.Session.GetString("UserInfo");
            if (userInfoJson != null)
            {
                var admin = JsonConvert.DeserializeObject<LoginCommon>(userInfoJson);
                var value = (admin.IsAdmin.ToString().ToLower() == "true" || admin.IsSupperUser.ToString().ToLower() == "true") ? "1" : "0";
                value = string.IsNullOrEmpty(value) ? "0" : value;
                return value;
            }
            return null;
        }

        public static string GetActions(string Control, string ExtraId = "", string AddEdit = "", string ExtraId1 = "", string ExtraId2 = "", string ExtraId3 = "")
        {
            var link = "";
            var enc = Base64Encode_URL(ExtraId.ToString());
            if (Control.ToLower() == "driversetup")
            {
                link += "<a href='/Admin/DriverSetup/UpdateDriverSetup?code=" + enc + "'  class='btn-action' title='Edit'><i class='bx bxs-pencil' style='color:black'></i></a>";
            }
            else if (Control.ToLower() == "bannersetup")
            {
                link += "<a href='/Admin/BannerSetup/UpdateBannerSetup?code=" + enc + "'  class='btn-action' title='Edit'><i class='bx bxs-pencil' style='color:blue'></i></a>";
                link += "<a href='/Admin/BannerSetup/UpdateBannerStatus?code=" + enc + "'  class='btn-action' title='change status'><i class='bx bx-reset' style='color:red'></i></a>";
            }
            else if (Control.ToLower() == "vechiclesetup")
            {
                link += "<a href='/Admin/VechicleInformation/VechicleIndex?VechicleType=" + ExtraId + "'  class='btn-action' title='view'><i class='bx bxs-right-arrow-circle' style='color:black'></i></a>";
            }
            else if (Control.ToLower() == "vechicletypesetup")
            {
                link += "<a href='/Admin/VechicleInformation/UpdateVechicleDetail?Code=" + enc + "'  class='btn-action' title='edit'><i class='bx bxs-pencil' style='color:black'></i></a>";
            }
            else if (Control.ToLower() == "guidesetup")
            {
                link += "<a href='/Admin/GuideSetup/UpdateGuideDetail?Code=" + enc + "'  class='btn-action' title='edit'><i class='bx bxs-pencil' style='color:black'></i></a>";
            }
            else if (Control.ToLower() == "hotelsetup")
            {
                link += "<a href='/Admin/HotelSetup/HotelList?accommodationType=" + ExtraId + "'  class='btn-action' title='view'><i class='bx bxs-right-arrow-circle' style='color:black'></i></a>";
            }
            else if (Control.ToLower() == "hotelinformation")
            {
                link += "<a href='/Admin/HotelSetup/UpdateHotelSetup?code=" + enc + "'  class='btn-action' title='edit'><i class='bx bxs-pencil' style='color:black'></i></a>";
                link += "<a href='/Admin/HotelSetup/UploadHotelGallery?code=" + enc + "'  class='btn-action' title='gallery'><i class='bx bx-image-add' style='color:red'></i></a>";
            }
            else if (Control.ToLower() == "bookinglist")
            {
                if (AddEdit.ToLower() == "pending")
                {

                }
                else if (AddEdit.ToLower() == "approved")
                {
                    link += "<a href='/Admin/HotelSetup/UploadHotelGallery?code=" + enc + "'  class='btn-action' title='gallery'><i class='fa-solid fa-money-bill' style='color:red'></i></a>";
 
                }
                else if (AddEdit.ToLower() == "booked")
                {
                    if (ExtraId1.ToLower() == "package")
                    {
                        link += "<a href='/Home/PackageBookedDetail?code=" + enc + "'  class='btn-action' title='view'><i class='fa-solid fa-eye' style='color:black'></i></a>";
                    }
                    else
                    {
                        link += "<a href='/Home/Confirmation?code=" + enc + "'  class='btn-action' title='view'><i class='fa-solid fa-eye' style='color:black'></i></a>";
                    }
            
                }
            }
            else if (Control.ToLower() == "blogsetup")
            {
                link += "<a href='/Admin/BlogSetup/UpdateBlogSetup?code=" + enc + "'  class='btn-action' title='Edit'><i class='bx bxs-pencil' style='color:blue'></i></a>";
                link += "<a href='/Admin/BlogSetup/UpdateBlogStatus?code=" + enc + "'  class='btn-action' title='change status'><i class='bx bx-reset' style='color:red'></i></a>";
            }
            else if (Control.ToLower() == "packagesetup")
            {
                link += "<a href='/Admin/PackageSetup/InformationList?PackageType=" + ExtraId + "'  class='btn-action' title='view'><i class='bx bxs-right-arrow-circle' style='color:black'></i></a>";
            }
            else if (Control.ToLower() == "packageinformation")
            {
                link += "<a href='/Admin/PackageSetup/UpdatePackageDetails?Code=" + enc + "'  class='btn-action' title='edit'><i class='bx bxs-pencil' style='color:black'></i></a>";
            }
            return link;
        }
        private static string SaltKey = "Pr@ldAD";
        public static string Base64Encode(string plainText)
        {
            plainText = plainText + SaltKey;
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                base64EncodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                base64EncodedData = base64EncodedData.Replace(SaltKey, "");
            }
            catch (Exception e)
            {
                base64EncodedData = "";
            }
            return base64EncodedData;
        }
        static string salt1 = "&%$%#@#";
        static string salt2 = "@$^#%^";
        public static string Base64Encode_URL(string plainText)
        {
            var enc = "";
            try
            {
                plainText = salt1 + plainText + salt2;
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                enc = System.Convert.ToBase64String(plainTextBytes);
                enc = enc.Replace("=", "000");
            }
            catch (Exception)
            {
                enc = "";
            }
            return enc;
        }
        public static string Base64Decode_URL(string base64EncodedData)
        {
            if (base64EncodedData == "Index" || string.IsNullOrWhiteSpace(base64EncodedData))
            {
                return "";
            }
            try
            {
                base64EncodedData = base64EncodedData.Replace("000", "=");
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                base64EncodedData = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                base64EncodedData = base64EncodedData.Replace(salt1, "");
                base64EncodedData = base64EncodedData.Replace(salt2, "");
            }
            catch (Exception e)
            {
                base64EncodedData = "";
            }
            return base64EncodedData;
        }
        public static string FrontToDBDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string[] date = null;
                if (value.Contains("-"))
                {
                    date = value.Substring(0, 10).Split('-');
                    return date[1] + "/" + date[2] + "/" + date[0];
                }
                else
                {
                    if (value.Length == 9)
                    {
                        date = value.Split('/');
                    }
                    else
                    {
                        date = value.Substring(0, 10).Split('/');
                    }
                }
                return date[1] + "/" + date[0] + "/" + date[2].Split(' ')[0];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="value">Accept date from DB in MM/dd/yyyy</param>
        /// <returns>returns Date in: dd/MM/yyyy</returns>
        public static string DBToFrontDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            try
            {
                string sysFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                if (sysFormat.ToLower().Contains("dd/mm/yyyy"))
                {
                    return value.Substring(0, 10);
                }
                var validDate = value.Split(' ');
                string[] date = validDate[0].Split('/');
                return (date[1].Length == 1 ? "0" + date[1] : date[1]) + "/" + (date[0].Length == 1 ? "0" + date[0] : date[0]) + "/" + date[2];
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}
