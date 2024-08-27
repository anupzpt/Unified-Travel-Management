using HotelApp.Shared.Admin.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HotelApp.Shared.Admin.Package
{
    public class PackageCommon : CommonModel
    {
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string PackageType { get; set; }
        public string PackageAverageCost { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public string BestSeason { get; set; }
        public string Group { get; set; }
        public string MinimumPerson { get; set; }
        public string MaximumPerson { get; set; }
        public string MaxAltiude { get; set; }
        public string Accommodation { get; set; }
        public string Transportation { get; set; }
        public string PackageCount { get; set; }
        public string Description { get; set; }
        public string GuideCode { get; set; }
        public string Status { get; set; }
        public string RouteImage { get; set; }
        public string PackageImageView { get; set; }
        public string RouteImageView { get; set; }
        public string DocumentCode { get; set; }
        public string ReviewCode { get; set; }
        public string GuideName {get;set;}
        public string GuideAge {get;set;}
        public string GuideAddress {get;set;}
        public string GuideExperience {get;set;}
        public string GuidePhoneNo {get;set;}
        public string GuideCitizenshipNo {get;set;}
        public string GuideSpecializedRegion {get;set;}
        public string GuideImageView {get;set;}
        public string GuideDescription { get; set; }
        public string RandomNumber { get; set; }
        public IFormFile PackageImageFile { get; set; }
        public IFormFile RouteImageFile { get; set; }
        public List<PackageItineraryDetails> PackageItineraries { get; set; }
        public List<PackageAvailabilityDetails> PackageAvailabilities { get; set; }
        public List<InclusionExcludesPackageDetails> InclusionExcludesPackage { get; set; }
        public List<SelectListItem> PackageTypeList { get; set; }
        public List<SelectListItem> GroupTypeList { get; set; }
        public List<SelectListItem> TransportationList { get; set; }
        public List<SelectListItem> AccommodationTypeList { get; set; }
        public List<SelectListItem> GuideList { get; set; }
        public string CreatedDate { get; set; }
    }
    public class PackageDetails
    {
        public string RowNum { get; set; }
        public string PackageType { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string Status{ get; set; }
        public string Action { get; set; }
        public string FilterCount { get; set; }
    }
    public class PackageItineraryDetails
    {
        public string Title { get; set; }
        public string Accomodation { get; set; }
        public string HotelCode { get; set; }
        public string HotelTypeCode { get; set; }
        public string Meals { get; set; }
        public string Transport { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
    }
    public class PackageAvailabilityDetails
    {
        public string PackageCode { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string AvailableDate { get; set; }
        public string PackageCost { get; set; }
    }
    public class InclusionExcludesPackageDetails
    {
        public string PackageCode { get; set; }
        public string IncludeExclude { get; set; }
        public string Description { get; set; }
    }
    public class PackageParamDetail
    {
        public string UserName { get; set; }
        public string Flag { get; set; }
        public string PackageCode { get; set; }
        public string PackageName { get; set; }
        public string PackageType { get; set; }
        public string PackageAverageCost { get; set; }
        public string Country { get; set; }
        public string Duration { get; set; }
        public string BestSeason { get; set; }
        public string Group { get; set; }
        public string MinimumPerson { get; set; }
        public string MaximumPerson { get; set; }
        public string MaxAltiude { get; set; }
        public string Accommodation { get; set; }
        public string Transportation { get; set; }
        public string PackageCount { get; set; }
        public string Description { get; set; }
        public string GuideCode { get; set; }
        public string Status { get; set; }
        public string RouteImageView { get; set; }
        public string PackageImageView { get; set; }
        public string PackageItinerariesJson { get; set; }
        public string PackageAvailabilitiesJson { get; set; }
        public string InclusionExcludesPackageJson { get; set; }
    }
}
