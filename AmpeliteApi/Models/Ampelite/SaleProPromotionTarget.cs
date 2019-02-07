using System;
namespace AmpeliteApi.Models.Ampelite
{
    public class SaleProPromotionTarget
    {
        public int TargetID { get; set; }
        public decimal Target { get; set; }
        public int Unit { get; set; }
        public string UnitDesc { get; set; }
        public string Description { get; set; }
        public decimal? Reward { get; set; }
        public decimal? Discount { get; set; }
        public decimal? GiftVoucher { get; set; }
        public decimal? Bonus { get; set; }
        public bool IsBonus { get; set; }
        public string CostPromotion { get; set; }
        public bool Status { get; set; }
        public string SubID { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
