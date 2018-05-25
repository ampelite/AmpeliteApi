using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class SaleproTrussScw
    {
        public int Id { get; set; }
        public string CustCodeId { get; set; }
        public string CustCode { get; set; }
        public string SubId { get; set; }
        public string CustName { get; set; }
        public decimal? GoodAmnt { get; set; }
        public DateTime? ShipDate { get; set; }
        public string ProductCode { get; set; }
        public string SumText { get; set; }
    }
}
