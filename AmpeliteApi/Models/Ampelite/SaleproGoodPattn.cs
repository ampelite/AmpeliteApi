using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class SaleproGoodPattn
    {
        public int Id { get; set; }
        public string GoodPattnCode { get; set; }
        public string GoodPattnName { get; set; }
        public string GoodClassCode { get; set; }
        public string GoodClassName { get; set; }
        public decimal? FactorCp { get; set; }
        public string SubId { get; set; }
        public string SubCodePro { get; set; }
        public string Status { get; set; }
    }
}
