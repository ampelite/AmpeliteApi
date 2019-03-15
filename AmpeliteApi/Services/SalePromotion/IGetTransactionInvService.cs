using AmpeliteApi.Data;
using AmpeliteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmpeliteApi.Services.SalePromotion
{
    public interface IGetTransactionInvService
    {
        List<DropDowns> PattnDropDowns();
        List<DropDowns> ClassDropDowns();
        List<DropDowns> ProductDropDowns();

        List<DropDowns> GoodCodeDropDowns();
        List<DropDowns> GoodBrandDropDowns();
    }

    public class GetTransactionInvService : IGetTransactionInvService
    {
        private readonly db_AmpeliteContext ctx;
        public GetTransactionInvService(db_AmpeliteContext context)
        {
            ctx = context;
        }

        public IEnumerable<GetTransactionInv> GetTransactionInvActive()
        {
            return ctx.GetTransactionInv;
        }

        public List<DropDowns> PattnDropDowns()
        {
            var list = GetTransactionInvActive()
                .Where(w => w.GoodPattnCode != null)
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

        public List<DropDowns> ClassDropDowns()
        {
            var list = GetTransactionInvActive()
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

        public List<DropDowns> ProductDropDowns()
        {
            var list = GetTransactionInvActive()
                .Where(w => w.Product != null)
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

        public List<DropDowns> GoodCodeDropDowns()
        {
            var list = GetTransactionInvActive()
                .Where(w => w.GoodCode != null)
                .GroupBy(x => new
                {
                    x.GoodId,
                    x.GoodCode
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.GoodId,
                    Text = x.Key.GoodCode
                }).ToList();
            return list;
        }

        public List<DropDowns> GoodBrandDropDowns()
        {
            var list = GetTransactionInvActive()
                .Where(w => w.GoodBrandName != null)
                .GroupBy(x => new
                {
                    x.GoodBrandCode,
                    x.GoodBrandName
                })
                .Select(x => new DropDowns
                {
                    Value = x.Key.GoodBrandCode,
                    Text = x.Key.GoodBrandName
                }).ToList();
            return list;
        }

    }
}
