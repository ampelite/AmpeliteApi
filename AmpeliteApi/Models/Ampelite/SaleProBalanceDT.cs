using System;
using System.ComponentModel;

namespace AmpeliteApi.Models.Ampelite
{
    public class SaleProBalanceDT
    {
        public int BDTID { get; set; }
        public int BHDID { get; set; }
        public int TargetID { get; set; }
        public decimal Target { get; set; }
        public decimal? Reward { get; set; }
        public decimal? GiftVoucher { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Bonus { get; set; }
        public bool IsBonus { get; set; }
        public int? Unit { get; set; }

        public bool RewardSelect { get; set; }
        public bool GiftSelect { get; set; }
        public bool DiscountSelect { get; set; }
        public bool BonusSelect { get; set; }

        public SaleProBalanceHD BalanceHD { get; set; }
    }
}
