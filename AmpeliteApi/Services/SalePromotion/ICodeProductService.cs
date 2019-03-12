using System;
using System.Linq;
using System.Collections.Generic;
using AmpeliteApi.Data;
using AmpeliteApi.Models;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface ICodeProductService
    {
        List<DropDowns> ProductnDropDowns();
    }

    public class CodeProductService : ICodeProductService
    {
        private readonly db_AmpeliteContext ctx;
        public CodeProductService(db_AmpeliteContext context)
        {
            ctx = context;
        }

        public IEnumerable<GetTransactionInv> ProductActive()
        {
            return ctx.GetTransactionInv;
        }

        public List<DropDowns> ProductnDropDowns()
        {
            var list = ProductActive()
                .GroupBy(x => new
                {
                    x.ProductCode,
                    x.Product
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.ProductCode,
                    Text = x.Key.Product
                }).ToList();
            return list;
        }
    }

}
