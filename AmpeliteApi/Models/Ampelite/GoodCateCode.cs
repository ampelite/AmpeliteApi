using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class GoodCateCode
    {
        public int Id { get; set; }
        public string GoodCatecode { get; set; }
        public string GoodCateName { get; set; }
        public string SubId { get; set; }
        public string SubCodePro { get; set; }
        public string Status { get; set; }
    }
}
