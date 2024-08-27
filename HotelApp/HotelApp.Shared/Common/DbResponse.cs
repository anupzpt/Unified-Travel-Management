﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Shared.Common
{
    public class DbResponse
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string? Extra { get; set; }
        public string? Extra2 { get; set; }
        public string? Extra3 { get; set; }
    }
}
