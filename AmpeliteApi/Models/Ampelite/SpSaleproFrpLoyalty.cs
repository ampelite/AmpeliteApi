using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models.Ampelite
{
    public class SpSaleproFrpLoyalty
    {
        public Int64 Id { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public decimal Rf { get; set; }
        public decimal GoodQty2 { get; set; }
        public decimal GoodCompareQty { get; set; }
        public decimal GoodAmnt { get; set; }
    }
}
