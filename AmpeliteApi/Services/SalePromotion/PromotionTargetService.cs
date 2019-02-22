using System;
using System.Linq;
using System.Collections.Generic;
using AmpeliteApi.Data;
using AmpeliteApi.Models;
using AmpeliteApi.Models.Ampelite;

namespace AmpeliteApi.Services.SalePromotion
{
    public class PromotionTargetService : IPromotionTargetService
    {
        private readonly db_AmpeliteContext ctx;
        public PromotionTargetService(db_AmpeliteContext context)
        {
            ctx = context;
        }

        public IEnumerable<SaleProPromotionTarget> SaleProPromotionTargetActive()
        {
            return ctx.SaleProPromotionTargets.Where(x => x.Status == true);
        }

        public List<DropDowns> UnitDropDowns() {
            var dropDowns = SaleProPromotionTargetActive()
                .GroupBy(x => new 
                {
                    x.Unit,
                    x.UnitDesc
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.Unit.ToString(),
                    Text = x.Key.UnitDesc
                }).ToList();

            return dropDowns;
        }
    }
}
