using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class DailypoGroupUnit
    {
        public int UnitId { get; set; }
        public string GroupCode { get; set; }
        public string UnitCode { get; set; }
        public decimal? UnitValue { get; set; }
        public string UnitName { get; set; }
        public string UnitTitle { get; set; }
    }
}
