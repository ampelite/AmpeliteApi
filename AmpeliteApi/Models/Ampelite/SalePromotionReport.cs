using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models
{
    public partial class SalePromotionReport
    {
        public int Id { get; set; }
        public string CustCodeId { get; set; }
        public string CustCode { get; set; }
        public int SubId { get; set; }
        public string CustName { get; set; }
        public decimal? GoodAmnt { get; set; }
        public DateTime? ShipDate { get; set; }
        public string ProductCode { get; set; }
    }
}
