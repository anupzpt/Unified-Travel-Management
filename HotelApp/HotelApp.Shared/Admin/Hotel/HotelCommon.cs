using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin.Hotel
{
    public class HotelCommon : CommonModel
    {
        public string Flag { get; set; }
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ReviewCode { get; set; }
        public string DocumentCode { get; set; }
        public string AccommodationType { get; set; }
        public string HotelImageView { get; set; }
        public string ContactNo { get; set; }
        public string AlternativeContactNo { get; set; }
        public string HotelAvailabilityJson { get; set; }
        public string HotelPropertySurroundingJson { get; set; }
        public string HotelFacilityJson { get; set; }
        public string Status { get; set; }
        public string RandomNumber { get; set; }
        public string TotalPrice { get; set; }
        public IFormFile HotelImageFile { get; set; }
        public List<SelectListItem> AccommodationTypeList { get; set; }
        public List<HotelAvailabilityDetails> HotelAvailabilityList { get; set; }
        public List<HotelPropertySurroundingDetails> HotelPropertySurroundingList { get; set; }
        public List<HotelFacilityDetails> HotelFacilityList { get; set; }
        public List<HotelGalleryDetails> HotelGalleryList { get; set; }
        public List<SelectListItem> RoomTypeList { get; set; }
        public List<SelectListItem> PropertyTypeList { get; set; }
        public List<SelectListItem> FacilityTypeList { get; set; }
    }
    public class HotelAvailabilityDetails
    {
        public string RoomType { get; set; }
        public string NumberOfGuest { get; set; }
        public string Price { get; set; }
        public string TotalNoRoom { get; set; }
        public string DiscountPercent { get; set; }
        public string TotalPrice { get; set; }
        public string RemaningRooms { get; set; }
        public string NoOfBed { get; set; }
        public string RoomNo { get; set; }
        public string Description { get; set; }
    }
    public class HotelPropertySurroundingDetails
    {
        public string PropertyType { get; set; }
        public string Name { get; set; }
    }
    public class HotelFacilityDetails
    {
        public string FacilityType { get; set; }
        public string Name { get; set; }
        public bool IsPopularFaculity { get; set; }
    }
    public class HotelGalleryDetails
    {
        public string HotelGalleryView { get; set; }
        public IFormFile HotelGalleryFile { get; set; }

    }
    public class HotelBookedList : CommonModel
    {
        public string RowNum { get; set; }
        public string HotelCode { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string HotelName { get; set; }
        public string BookedBy { get; set; }
        public string BookedDate { get; set; }
        public string Status { get; set; }
    }
}
