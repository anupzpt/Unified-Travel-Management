using HotelApp.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Admin
{
    public class FacilityDetails: GridDetails
    {
        public string RowNum { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string Action { get; set; }
        public string FilterCount { get; set; }
    }
}
