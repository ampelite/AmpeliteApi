using System;
using System.Collections.Generic;

namespace AmpeliteApi.Models.Ampelite
{
    public class SaleProBalanceHD
    {
        public int BHDID { get; set; }
        public string SUBID { get; set; }
        public bool IsConfirm { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal GoodQty2 { get; set; }
        public decimal GoodAmnt { get; set; }
        public decimal? TotalReward { get; set; }
        public decimal? TotalGiftVoucher { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalBonus { get; set; }
        public ICollection<SaleProBalanceDT> BalancesDT { get; set; }
    }
}
