using System;
using System.Collections.Generic;
using System.Linq;
using AmpeliteApi.Data;
using AmpeliteApi.Models;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface ICodePromotionService
    {
        List<DropDowns> MainPromotionDropDowns();

        List<DropDowns> SubPromotionDropDowns();

        List<DropDowns> SubPromotionWithMainProDropDowns(string mainPro);
    }

    public class CodePromotionService : ICodePromotionService
    {
        private readonly db_AmpeliteContext ctx;
        public CodePromotionService(db_AmpeliteContext context)
        {
            ctx = context;
        }

        public IEnumerable<CodePromotion> CodePromotionsActive()
        {
            return ctx.CodePromotion.Where(x => x.Status == true);
        }

        public List<DropDowns> MainPromotionDropDowns()
        {
            var list = CodePromotionsActive()
                .GroupBy(x => new
                {
                    x.CodeMainPro,
                    x.MainPro
                })
               .Select(x => new DropDowns
               {
                   Value = x.Key.CodeMainPro.ToString(),
                   Text = x.Key.MainPro
               }).ToList();
            return list;
        }

        public List<DropDowns> SubPromotionDropDowns()
        {
            var list = CodePromotionsActive()
                .GroupBy(x => new
                {
                    x.SubId,
                    x.SubPromotion
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.SubId,
                    Text = x.Key.SubPromotion
                }).ToList();
            return list;
        }

        public List<DropDowns> SubPromotionWithMainProDropDowns(string mainPro)
        {
            var list = CodePromotionsActive()
                 .Where(x => x.MainPro == mainPro)
                 .GroupBy(x => new
                 {
                     x.SubId,
                     x.SubPromotion
                 })
                  .Select(x => new DropDowns
                  {
                      Value = x.Key.SubId,
                      Text = x.Key.SubPromotion
                  }).ToList();
            return list;
        }
    }

}
