using AmpeliteApi.Data;
using AmpeliteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface ICodeClassService
    {
        List<DropDowns> ClassDropDowns();
    }

    public class CodeClassService : ICodeClassService
    {
        private readonly db_AmpeliteContext ctx;
        public CodeClassService(db_AmpeliteContext context)
        {
            ctx = context;
        }

        public IEnumerable<GetTransactionInv> ClassActive()
        {
            return ctx.GetTransactionInv;
        }

        public List<DropDowns> ClassDropDowns()
        {
            var list = ClassActive()
                .Where(w => w.GoodClassCode != null)
                .GroupBy(x => new
                {
                    x.GoodClassCode,
                    x.GoodClassName
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.GoodClassCode,
                    Text = x.Key.GoodClassName
                }).ToList();
            return list;
        }
    }

}