using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class DailypoProductGroup
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string Product { get; set; }
        public string GoodBrandCode { get; set; }
        public string GoodBrandName { get; set; }
        public string GroupBrandCode { get; set; }
        public string GroupBrandName { get; set; }
        public string ProductType { get; set; }
    }
}
