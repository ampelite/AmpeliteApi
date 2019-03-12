using AmpeliteApi.Data;
using AmpeliteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface ICodePattnService
    {
        List<DropDowns> PattnDropDowns();
    }

    public class CodePattnService : ICodePattnService
    {
        private readonly db_AmpeliteContext ctx;
        public CodePattnService(db_AmpeliteContext context)
        {
            ctx = context;
        }

        public IEnumerable<GetTransactionInv> PattnActive()
        {
            return ctx.GetTransactionInv;
        }

        public List<DropDowns> PattnDropDowns()
        {
            var list = PattnActive()
                .GroupBy(x => new
                {
                    x.GoodPattnCode,
                    x.GoodPattnName
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.GoodPattnCode,
                    Text = x.Key.GoodPattnName
                }).ToList();
            return list;
        }
    }

}
