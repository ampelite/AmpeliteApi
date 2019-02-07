using System;
namespace AmpeliteApi.Models.Ampelite
{
    public class SaleProBalanceDT
    {
        public int TargetID { get; set; }
        public decimal Target { get; set; }
        public decimal? Reward { get; set; }
        public decimal? GiftVoucher { get; set; }
        public decimal? Discount { get; set; }
        public bool IsBonus { get; set; }
        public decimal? Bonus { get; set; }
        public int Unit { get; set; }
    }
}
