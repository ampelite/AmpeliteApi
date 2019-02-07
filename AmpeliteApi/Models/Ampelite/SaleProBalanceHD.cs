using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models.Ampelite
{
    public class SaleProBalanceHD
    {
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public decimal GoodQty2 { get; set; }
        public decimal GoodAmnt { get; set; }
        public decimal? TotalReward { get; set; }
        public decimal? TotalGiftVoucher { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalBonus { get; set; }
        public List<SaleProBalanceDT> BalancesDT { get; set; }
    }
}
